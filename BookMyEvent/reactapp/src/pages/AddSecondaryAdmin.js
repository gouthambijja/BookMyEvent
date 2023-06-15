import React, { useEffect, useState } from 'react';
import { TextField, Button,Typography,Container } from '@mui/material';
import { useSelector } from 'react-redux';
import { makeStyles } from "@material-ui/core/styles";
import store from '../App/store';
import { Box, Modal } from "@mui/material";
import { addAdmin } from "../Services/AdminServices";


const useStyles = makeStyles((theme) => ({
    container: {
      display: "flex",
      flexDirection: "column",
      height: "calc( 100vh - 64px)",
      alignItems: "center",
      justifyContent: "center",
    },
    form: {
      width: "100%",
      marginTop: theme.spacing(1),
    },
    submitButton: {
      margin: theme.spacing(3, 0, 2),
    },
  }));
  

const AddSecondaryAdmin = () => {
  const classes = useStyles();
    const admin = store.getState().admin.profile;
    const [image, setImage] = useState(null);
    const [formData, setFormData] = useState({
        AdministratorName: 'dad',
        AdministratorAddress: 'asddf',
        Email: "kaka@gmail.c",
        PhoneNumber: "33",
        CreatedOn: (new Date()).toLocaleString(),
        UpdatedOn: (new Date()).toLocaleString(),
        RoleId: 1,
        IsAccepted: true,
        ImageName: "profile",
        CreatedBy: admin.administratorId,
        AcceptedBy: admin.administratorId,
        OrganisationId: admin.organisationId,
        IsActive: true,
        Password: "kaka",
    });
    
    const handleImageChange = async(e) => {
        const file = e.target.files[0];
        setImage(file);
        setFormData((prevState) => ({
            ...prevState,
            ImgBody: file,
        }));
    };
    const handleInputChange = (e) => {
        setFormData((prevState) => ({
            ...prevState,
            [e.target.name]: e.target.value,
        }));
    };
    const handleSubmit = async (e) => {
        e.preventDefault();
        console.log("before post call "+formData)
        const _formData = new FormData();
        _formData.append("administratorName",formData.AdministratorName);
        _formData.append("administratorAddress",formData.AdministratorAddress);
        _formData.append("email",formData.Email);
        _formData.append("phoneNumber",formData.PhoneNumber);
        _formData.append("createdOn",formData.CreatedOn);
        _formData.append("updatedOn",formData.UpdatedOn);
        _formData.append("roleId",1);
        _formData.append("isAccepted",true);
        _formData.append("imageName",formData.profile);
        _formData.append("createdBy",formData.CreatedBy);
        _formData.append("acceptedBy",formData.AcceptedBy);
        _formData.append("organisationId",formData.OrganisationId);
        _formData.append("isActive",true);
        _formData.append("password",formData.Password);
        _formData.append("imgBody",formData.ImgBody);
        const data=await addAdmin(_formData);
        // Perform registration logic here, e.g., make an API call
        console.log("after call "+ data)
        // Reset form fields
       setFormData({AdministratorName: "",
       AdministratorAddress: "",
       Email: "",
       PhoneNumber: "",
       CreatedOn: new Date(),
       UpdatedOn: new Date(),
       RoleId: 1,
       IsAccepted: true,
       ImageName: "profile",
       ImgBody:null,
       CreatedBy: admin.id,
       AcceptedBy: admin.id,
       OrganisationId: admin.OrganisationId,
       IsActive: true,
       Password: "",})
    };
    useEffect(()=>{
        console.log(admin);
    },[])
    return (
        <>
      <Container component="main" maxWidth="xl" className={classes.container}>
      <Box
          sx={{ boxShadow: 3, p: 3, borderRadius: "16px", textAlign: "center" }}
        >
        <Typography variant="h5" component="h1" border={1}>
            Add New Admin
          </Typography>
        <form className={classes.form} onSubmit={handleSubmit}>
            <TextField
                label="Name"
                id="Name"
                name="AdministratorName"
                variant="outlined"
                value={formData.AdministratorName}
                onChange={handleInputChange}
                margin="normal"
                required
                fullWidth
            />
            <TextField
                label="Address"
                name='AdministratorAddress'
                id='Address'
                variant="outlined"
                value={formData.AdministratorAddress}
                onChange={handleInputChange}
                margin="normal"
                required
                fullWidth
            />
            <TextField
                label="Email"
                id='Email'
                name="Email"
                variant="outlined"
                type="email"
                value={formData.Email}
                onChange={handleInputChange}
                margin="normal"
                required
                fullWidth
            />
            <TextField
                label="Phone Number:"
                id='Phone Number'
                name='PhoneNumber'
                variant="outlined"
                type="number"
                value={formData.PhoneNumber}
                onChange={handleInputChange}
                margin="normal"
                required
                fullWidth
            />


            <TextField
                label="Password"
                id='Password'
                name='Password'
                variant="outlined"
                type="password"
                value={formData.Password}
                onChange={handleInputChange}
                margin="normal"
                required
                fullWidth
            />

            <input
                accept="image/jpeg"
                id="image-upload"
                type="file"
                onChange={handleImageChange}
                style={{ display: 'none' }}
            />
            <label htmlFor="image-upload">
                <Button component="span" variant="contained" color="primary" fullWidth>
                    Upload Image
                </Button>
            </label>
            {image && <p>Selected image: {image.name}</p>}
            <Button type="submit" variant="contained" className={classes.submitButton} color="primary" fullWidth>
                Register
            </Button>
        </form>
        </Box>
        </Container>
        </>
    );
};

export default AddSecondaryAdmin;
