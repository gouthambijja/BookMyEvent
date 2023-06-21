import React, { useEffect, useState } from 'react';
import { TextField, Button, Typography, Container, FormControl, Select, MenuItem, InputLabel, Autocomplete } from '@mui/material';
import { useSelector } from 'react-redux';
import { makeStyles } from "@material-ui/core/styles";
import store from '../App/store';
import { Box, Modal } from "@mui/material";
import org from "../Services/OrganisationService";
import { width } from '@mui/system';
import userServices from '../Services/UserServices';
import { useNavigate } from 'react-router-dom';


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


const RegisterUser = () => {
    const classes = useStyles();
    // const admin = store.getState().admin.profile;
    const [image, setImage] = useState(null);
    const [passwordError, setPasswordError] = useState('');
    const [emailError, setEmailError] = useState('');
    const [selectedOption, setSelectedOption] = useState('');
    const [confirmPassword, setConfirmPassword] = useState('');
    const navigate = useNavigate();
    const [formData, setFormData] = useState({
        Name: '',
        Email: "",
        PhoneNumber: "",
        UserAddress: '',
        CreatedOn: (new Date()).toLocaleString(),
        UpdatedOn: (new Date()).toLocaleString(),
        IsActive: true,
        ImageName: "profile",
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
    const handleConfirmPassword = (e) => {
        setConfirmPassword(e.target.value)
        if (e.target.value !== formData.Password) {
            setPasswordError('Passwords do not match');
        }
        else {
            // Passwords match, perform form submission or further processing
            setPasswordError('');
            // ...
        }
    }

    const handleInputChange = (e) => {
        setFormData((prevState) => ({
            ...prevState,
            [e.target.name]: e.target.value,
        }));
    };
    const handleEmailChange = async (e) => {
        setFormData((prevState) => ({
            ...prevState,
            [e.target.name]: e.target.value,
        }));
        // if (e.target.value.includes('@')) {

        //     const response = await organiserServices.checkEmail(e.target.value);
        //     if (response.isEmailTaken) {
        //         setEmailError("Email already exists");
        //     }
        //     else {
        //         setEmailError("");
        //     }
        // }
    }


    const handleSubmit = async (e) => {
        e.preventDefault();
        console.log("before post call " + formData)
        const _formData = new FormData();     
            _formData.append("Name", formData.Name);
            _formData.append("email", formData.Email);
            _formData.append("phoneNumber", formData.PhoneNumber);
            _formData.append("userAddress", formData.UserAddress);
            _formData.append("createdOn", formData.CreatedOn);
            _formData.append("updatedOn", formData.UpdatedOn);
            _formData.append("imageName", formData.ImageName);
            _formData.append("isActive", true);
            _formData.append("password", formData.Password);
            _formData.append("imgBody", formData.ImgBody);
            console.log(_formData);
            const data = await userServices.registerUser(_formData);
            console.log("after call " + data)
        setFormData({
            Name: "",
           UserAddress: "",
            Email: "",
            PhoneNumber: "",
            CreatedOn: new Date(),
            UpdatedOn: new Date(),
            ImageName: "profile",
            ImgBody: null,
            IsActive: true,
            Password: "",
        })
        navigate("/login");
    };
    return (
        <>
            <Container component="main" maxWidth="xl" className={classes.container}>
                <Box
                    sx={{ boxShadow: 3, p: 3, borderRadius: "16px", textAlign: "center" }}
                >
                    <Typography variant="h5" component="h1" border={1}>
                        Register
                    </Typography>
                    <form className={classes.form} onSubmit={handleSubmit}>
                        <FormControl variant="outlined" margin="normal" fullWidth>

                            <TextField
                                label="Name"
                                id="Name"
                                name="Name"
                                variant="outlined"
                                value={formData.Name}
                                onChange={handleInputChange}
                                margin="normal"
                                required
                                fullWidth
                            />
                        </FormControl>
                        <TextField
                            label="Email"
                            id='Email'
                            name="Email"
                            variant="outlined"
                            type="email"
                            value={formData.Email}
                            onChange={handleEmailChange}
                            // error={emailError !== ''}
                            // helperText={emailError}
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
                            label="Address"
                            name='UserAddress'
                            id='Address'
                            variant="outlined"
                            value={formData.UserAddress}
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

                        <TextField
                            label="ConfirmPassword"
                            id='ConfirmPassword'
                            name='Password'
                            variant="outlined"
                            type="password"
                            error={passwordError !== ''}
                            helperText={passwordError}
                            value={confirmPassword}
                            onChange={handleConfirmPassword}
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
                                Upload Profile Picture
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

export default RegisterUser;
