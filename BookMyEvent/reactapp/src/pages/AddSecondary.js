import React, { useEffect, useState } from 'react';
import { TextField, Button, Typography, Container } from '@mui/material';
import { useSelector } from 'react-redux';
import { makeStyles } from "@material-ui/core/styles";
import store from '../App/store';
import { Box, Modal } from "@mui/material";
import { addAdmin } from "../Services/AdminServices";
import organiserServices from '../Services/OrganiserServices';


const useStyles = makeStyles((theme) => ({
    container: {
        display: "flex",
        flexDirection: "column",
        height: "calc( 100vh - 64px)",
        alignItems: "center",
        justifyContent: "center",
    },
    form: {
        width: "50%",
        marginTop: theme.spacing(1),
    },
    submitButton: {
        margin: theme.spacing(3, 0, 2),
    },
}));


const AddSecondary = () => {
    const classes = useStyles();
    const profile = store.getState().profile.info;
    const auth = store.getState().auth;
    const [image, setImage] = useState(null);
    const [emailError, setEmailError] = useState('');
    const [formData, setFormData] = useState({
        AdministratorName: '',
        AdministratorAddress: '',
        Email: "",
        PhoneNumber: "",
        CreatedOn: (new Date()).toLocaleString(),
        UpdatedOn: (new Date()).toLocaleString(),
        RoleId: profile.roleId == 2 ? 3 : 1,
        IsAccepted: true,
        ImageName: "profile",
        CreatedBy: profile.administratorId,
        AcceptedBy: profile.administratorId,
        OrganisationId: profile.organisationId,
        IsActive: true,
        Password: "",
    });

    const handleImageChange = async (e) => {
        const file = e.target.files[0];
        setImage(file);
        setFormData((prevState) => ({
            ...prevState,
            ImgBody: file,
        }));
    };
    const handleEmailChange = async (e) => {
        setFormData((prevState) => ({
            ...prevState,
            [e.target.name]: e.target.value,
        }));
        if (e.target.type == 'email' && e.target.value != '') {
            const response = await organiserServices.checkEmail(e.target.value);
            if (response.isEmailTaken) {
                setEmailError("Email already exists");
            }
            else {
                setEmailError("");
            }
        }
    }
    const handleInputChange = (e) => {
        setFormData((prevState) => ({
            ...prevState,
            [e.target.name]: e.target.value,
        }));
    };
    const handleSubmit = async (e) => {
        e.preventDefault();
        console.log("before post call " + formData)
        const _formData = new FormData();
        _formData.append("administratorName", formData.AdministratorName);
        _formData.append("administratorAddress", formData.AdministratorAddress);
        _formData.append("email", formData.Email);
        _formData.append("phoneNumber", formData.PhoneNumber);
        _formData.append("createdOn", formData.CreatedOn);
        _formData.append("updatedOn", formData.UpdatedOn);
        _formData.append("roleId", formData.RoleId);
        _formData.append("isAccepted", true);
        _formData.append("imageName", formData.profile);
        _formData.append("createdBy", formData.CreatedBy);
        _formData.append("acceptedBy", formData.AcceptedBy);
        _formData.append("organisationId", formData.OrganisationId);
        _formData.append("isActive", true);
        _formData.append("password", formData.Password);
        _formData.append("imgBody", formData.ImgBody);

        let data;
        if (profile.roleId == 1) {
            data = await addAdmin(_formData);
        } else {
            data = await organiserServices.addOrganiser(_formData);
        }
        // Perform registration logic here, e.g., make an API call
        console.log("after call " + data)
        // Reset form fields
        setFormData({
            AdministratorName: "",
            AdministratorAddress: "",
            Email: "",
            PhoneNumber: "",
            CreatedOn: new Date(),
            UpdatedOn: new Date(),
            RoleId: '',
            IsAccepted: true,
            ImageName: "profile",
            ImgBody: null,
            CreatedBy: profile.administratorId,
            AcceptedBy: profile.administratorId,
            OrganisationId: profile.OrganisationId,
            IsActive: true,
            Password: "",
        })
    };
    useEffect(() => {
    }, [])
    return (
        <>
            <Container component="main" maxWidth="xl" className={classes.container}>
                <Box
                    sx={{ boxShadow: 3, p: 3, borderRadius: "16px", textAlign: "center" }}
                >
                    <div style={{ display: 'flex', justifyContent: 'center' }}>
                        <Typography variant="h5" component="h1" >
                            Add New {auth.role}
                        </Typography>
                    </div>
                    <div style={{ display: 'flex', justifyContent: 'center' }}>
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
                                onChange={handleEmailChange}
                                margin="normal"
                                error={emailError !== ''}
                                helperText={emailError}
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
                            <Button type="submit" variant="contained" sx={{ marginTop:'10px' }} className={classes.submitButton} color="primary" fullWidth>
                                Register
                            </Button>
                        </form>
                    </div>
                </Box>
            </Container>
        </>
    );
};

export default AddSecondary;
