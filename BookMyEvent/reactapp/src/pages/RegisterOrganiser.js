import React, { useEffect, useState } from 'react';
import { TextField, Button, Typography, Container, FormControl, Select, MenuItem, InputLabel, Autocomplete } from '@mui/material';
import { useSelector } from 'react-redux';
import { makeStyles } from "@material-ui/core/styles";
import store from '../App/store';
import { Box, Modal } from "@mui/material";
import { addAdmin } from "../Services/AdminServices";
import t from "../Services/OrganisationService";
import { width } from '@mui/system';
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
        width: "100%",
        marginTop: theme.spacing(1),
    },
    submitButton: {
        margin: theme.spacing(3, 0, 2),
    },
}));


const RegisterOrganiser = () => {
    const classes = useStyles();
    const admin = store.getState().admin.profile;
    const [image, setImage] = useState(null);
    const [passwordError, setPasswordError] = useState('');
    const [orgNameError, setOrgNameError] = useState('');
    const [selectedOption, setSelectedOption] = useState(null);
    const [confirmPassword, setConfirmPassword] = useState('');
    const [options, setOptions] = useState(null);
    const [organisations, setOrganisations] = useState([]);
    const [selectedRoleValue, setSelectedRoleValue] = useState('dummy');
    const [formData, setFormData] = useState({
        AdministratorName: '',
        AdministratorAddress: '',
        Email: "",
        PhoneNumber: "",
        CreatedOn: (new Date()).toLocaleString(),
        UpdatedOn: (new Date()).toLocaleString(),
        RoleId: 1,
        IsAccepted: true,
        ImageName: "profile",


        IsActive: true,
        Password: "",
    });
    const [orgFormData, setOrgFormData] = useState({
        OrganisationName: "",
        OrganisationDescription: "",
        Location: "",
        CreatedOn: (new Date()).toLocaleString(),
        IsActive: false,
        UpdatedOn: (new Date()).toLocaleString(),

    })


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



    const handleOptionChange = (event, value) => {
        setSelectedOption(value);
        console.log(value.value);
    };
    const handleInputChange = (e) => {
        setFormData((prevState) => ({
            ...prevState,
            [e.target.name]: e.target.value,
        }));
    };
    const handleOrgInputChange = (e) => {
        setOrgFormData((prevState) => ({
            ...prevState,
            [e.target.name]: e.target.value,
        }));
    };
    const handleOrgNameChange = (e) => {
        const isNameInOrganisation = Object.values(options).some(
            (org) => org.organisationName === e.target.value
        );
        if (isNameInOrganisation) {
            setOrgNameError("Organisation already exixts")
        }
        else {
            setOrgNameError('')
        }
        setOrgFormData((prevState) => ({
            ...prevState,
            [e.target.name]: e.target.value,
        }));
    }
    const handleRoleChange = (event) => {
        setSelectedRoleValue(event.target.value);
        options.forEach(e => {
            const newItem = { label: e.organisationName, value: e.organisationId, id: e.OrganisationId };
            setOrganisations((prevState) => [
                ...prevState,
                newItem
            ]);

        });
    };



    const handleSubmit = async (e) => {
        e.preventDefault();
        console.log("before post call " + formData)
        const _formData = new FormData();
        if (selectedRoleValue == "peer") {
            _formData.append("administratorName", formData.AdministratorName);
            _formData.append("administratorAddress", formData.AdministratorAddress);
            _formData.append("email", formData.Email);
            _formData.append("phoneNumber", formData.PhoneNumber);
            _formData.append("createdOn", formData.CreatedOn);
            _formData.append("updatedOn", formData.UpdatedOn);
            _formData.append("roleId", 4);
            _formData.append("isAccepted", false);
            _formData.append("imageName", formData.ImageName);
            _formData.append("organisationId", selectedOption.value);
            _formData.append("isActive", true);
            _formData.append("password", formData.Password);
            _formData.append("imgBody", formData.ImgBody);
            console.log(_formData);
            const data = await organiserServices.addPeer(_formData);
            console.log("after call " + data)
        }
        else {


            _formData.append("administratorName", formData.AdministratorName);
            _formData.append("administratorAddress", formData.AdministratorAddress);
            _formData.append("email", formData.Email);
            _formData.append("phoneNumber", formData.PhoneNumber);
            _formData.append("createdOn", formData.CreatedOn);
            _formData.append("updatedOn", formData.UpdatedOn);
            _formData.append("roleId", 2);
            _formData.append("isAccepted", false);
            _formData.append("imageName", formData.ImageName);
            _formData.append("isActive", true);
            _formData.append("password", formData.Password);
            _formData.append("imgBody", formData.ImgBody);
            _formData.append("organisationName", orgFormData.OrganisationName);
            _formData.append("organisationDescription", orgFormData.OrganisationDescription);
            _formData.append("Location", orgFormData.Location);
            _formData.append("orgcreatedOn", orgFormData.CreatedOn);
            _formData.append("orgIsActive", orgFormData.IsActive);
            _formData.append("orgUpdatedOn", orgFormData.UpdatedOn);

            console.log(_formData);
            const data = await organiserServices.addOwner(_formData);
            console.log("after call " + data)


        }
        // Perform registration logic here, e.g., make an API call
        // Reset form fields
        setFormData({
            AdministratorName: "",
            AdministratorAddress: "",
            Email: "",
            PhoneNumber: "",
            CreatedOn: new Date(),
            UpdatedOn: new Date(),
            RoleId: 1,
            IsAccepted: true,
            ImageName: "profile",
            ImgBody: null,
            CreatedBy: admin.id,
            AcceptedBy: admin.id,
            OrganisationId: admin.OrganisationId,
            IsActive: true,
            Password: "",
        })
    };
    useEffect(() => {
        const temp = async () => {
            console.log(admin);
            const options = await t.getAllOrganisations();
            setOptions(options)
            console.log(options);
        }
        temp();
    }, [])
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
                                name="AdministratorName"
                                variant="outlined"
                                value={formData.AdministratorName}
                                onChange={handleInputChange}
                                margin="normal"
                                required
                                fullWidth
                            />
                        </FormControl>
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
                                Upload Image
                            </Button>
                        </label>
                        {image && <p>Selected image: {image.name}</p>}


                        <FormControl required sx={{ width: '300px' }}>
                            <InputLabel htmlFor="role-select">Select an option</InputLabel>
                            <Select value={selectedRoleValue} labelId="role-select" id="role-select" onChange={handleRoleChange} >

                                <MenuItem value="dummy">Select an Option</MenuItem>
                                <MenuItem value="peer">Join an existing Organisation</MenuItem>
                                <MenuItem value="owner">Create a new Organisation</MenuItem>

                            </Select>
                        </FormControl>
                        {selectedRoleValue == "owner" ? (<>
                            <Typography variant="h5" gutterBottom>
                                Please fill in your organization details
                            </Typography>
                            <FormControl variant="outlined" margin="normal" fullWidth>

                                <TextField
                                    label="Name of the Organisation"
                                    id="Name"
                                    name="OrganisationName"
                                    variant="outlined"
                                    value={orgFormData.OrganisationName}
                                    onChange={handleOrgNameChange}
                                    margin="normal"
                                    error={orgNameError !== ''}
                                    helperText={orgNameError}
                                    required
                                    fullWidth
                                />
                            </FormControl>
                            <FormControl variant="outlined" margin="normal" fullWidth>
                                <TextField
                                    label="Description"
                                    id="Description"
                                    name="OrganisationDescription"
                                    variant="outlined"
                                    value={orgFormData.OrganisationDescription}
                                    onChange={handleOrgInputChange}
                                    margin="normal"
                                    required
                                    multiline
                                    rows={4}
                                    fullWidth
                                />
                            </FormControl>
                            <FormControl variant="outlined" margin="normal" fullWidth>

                                <TextField
                                    label="Location of the Organisation"
                                    id="Name"
                                    name="Location"
                                    variant="outlined"
                                    value={orgFormData.Location}
                                    onChange={handleOrgInputChange}
                                    margin="normal"
                                    required
                                    fullWidth
                                />
                            </FormControl>
                        </>) : (selectedRoleValue == "peer" ? (
                            <Autocomplete
                                options={organisations}
                                getOptionLabel={(option) => option.label}
                                renderInput={(params) => (
                                    <TextField {...params} label="Select your Organisation" variant="outlined" />
                                )}
                                onChange={handleOptionChange}
                                value={selectedOption}
                                autoHighlight
                                autoSelect
                                disableClearable
                                noOptionsText="No options found"
                                style={{ width: '300px' }}
                            />

                        ) : <></>
                        )}

                        <Button type="submit" variant="contained" className={classes.submitButton} color="primary" fullWidth>
                            Register
                        </Button>
                    </form>
                </Box>
            </Container>
        </>
    );
};

export default RegisterOrganiser;
