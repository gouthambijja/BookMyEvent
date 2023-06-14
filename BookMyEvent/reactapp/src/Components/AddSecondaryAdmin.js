import React, { useState } from 'react';
import { TextField, Button,Typography,Container } from '@mui/material';
import { useSelector } from 'react-redux';
import { makeStyles } from "@material-ui/core/styles";

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
  const style = {
    position: "absolute",
    top: "50%",
    left: "50%",
    transform: "translate(-50%, -50%)",
    width: 400,
    bgcolor: "background.paper",
    border: "2px solid #000",
    boxShadow: 24,
    p: 4,
  };

const AddSecondaryAdmin = () => {
  const classes = useStyles();
    const admin = useSelector((store) => store.admin);
    const [image, setImage] = useState(null);
    const [formData, setFormData] = useState({
        AdministratorName: "",
        AdministratorAddress: "",
        Email: "",
        PhoneNumber: "",
        CreatedOn: new Date(),
        UpdatedOn: new Date(),
        RoleId: 1,
        IsAccepted: true,
        ImageName: "profile",
        CreatedBy: admin.id,
        AcceptedBy: admin.id,
        OrganisationId: admin.OrganisationId,
        IsActive: true,
        Password: "",
    });
    function imageToByteArray(file) {
        return new Promise((resolve, reject) => {
          const reader = new FileReader();
      
          reader.onload = (event) => {
            const arrayBuffer = event.target.result;
            const byteArray = new Uint8Array(arrayBuffer);
            resolve(byteArray);
          };
      
          reader.onerror = (event) => {
            reject(new Error('Error converting image to byte array.'));
          };
      
          reader.readAsArrayBuffer(file);
        });
      }
    const handleImageChange = async(e) => {
        const file = e.target.files[0];
        setImage(file);
        const ffile = await imageToByteArray(file);
        setFormData((prevState) => ({
            ...prevState,
            ImgBody: ffile,
        }));
    };
    const handleInputChange = (e) => {
        setFormData((prevState) => ({
            ...prevState,
            [e.target.name]: e.target.value,
        }));
    };
    const handleSubmit = (e) => {
        e.preventDefault();
        console.log(formData)
        // Perform registration logic here, e.g., make an API call

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
       CreatedBy: admin.id,
       AcceptedBy: admin.id,
       OrganisationId: admin.OrganisationId,
       IsActive: true,
       Password: "",})
    };

    return (
        <>
      <Container component="main" maxWidth="xl" className={classes.container}>

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
        </Container>
        </>
    );
};

export default AddSecondaryAdmin;
