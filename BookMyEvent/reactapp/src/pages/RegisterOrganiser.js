import React, { useEffect, useState } from 'react';
import { TextField, Button, Typography, Container, FormControl, Select, MenuItem, InputLabel, Autocomplete } from '@mui/material';
import { useSelector } from 'react-redux';
import { makeStyles } from "@material-ui/core/styles";
import store from '../App/store';
import { Box, Modal } from "@mui/material";
import org from "../Services/OrganisationService";
import { width } from '@mui/system';
import organiserServices from '../Services/OrganiserServices';
import organisationServices from '../Services/OrganisationService';
import { useNavigate } from 'react-router-dom';
import { toast } from 'react-toastify';


const useStyles = makeStyles((theme) => ({
    container: {
        display: "flex",
        flexDirection: "column",
        alignItems: "center",
        justifyContent: "center",
        padding:'40px',
    },
    form: {
        width: "90%",
        maxWidth:"800px",
        marginTop: theme.spacing(1),
    },
    submitButton: {
        margin: theme.spacing(3, 0, 2),
    },
}));


const RegisterOrganiser = () => {
    const classes = useStyles();
    // const admin = store.getState().admin.profile;
    const [image, setImage] = useState(null);
    const [passwordError, setPasswordError] = useState('');
    const [orgNameError, setOrgNameError] = useState('');
    const [peerOrgNameError, setPeerOrgNameError] = useState('');
    const [emailError, setEmailError] = useState('');
    const [selectedOption, setSelectedOption] = useState('');
    const [confirmPassword, setConfirmPassword] = useState('');
    const [passwordValidation,setPasswordValidation]=useState('');
    const [imageError, setImageError] = useState('');
    const [isValid,setIsValid]=useState(false);
    const navigate = useNavigate();

    const [selectedRoleValue, setSelectedRoleValue] = useState('dummy');
    const passwordRegex = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*?&]{8,}$/;
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
        if (file) {
            // Perform image validations here
            const allowedTypes = ['image/jpeg', 'image/png','image/jpg'];
         
      
            if (!allowedTypes.includes(file.type)) {
              setImageError('Invalid file type. Please select a JPEG, PNG or JPG image.');
            } 
             else {
            
              setImageError('');
            }
          }
    };
    const handleConfirmPassword = (e) => {
        setConfirmPassword(e.target.value)
        if (e.target.value !== formData.Password) {
            setPasswordError('Passwords do not match');
        }
        else {
         
            setPasswordError('');
         
        }
    }


    const handleOptionChange = async (event, value) => {
        if (event.target?.value != "") {
            const result = await organisationServices().getOrganisationByName(event.target.value);
            if (result.isExists) {
                //console.log(result.orgId)
                setPeerOrgNameError("");
                setSelectedOption(result.orgId);
            }
            else {
                setPeerOrgNameError("Organisation does not exists")
            }
        }
        else {
            setPeerOrgNameError("");
        }
        //console.log(selectedOption);
    };
    const handleInputChange = (e) => {
        setFormData((prevState) => ({
            ...prevState,
            [e.target.name]: e.target.value,
        }));
    };
    const handlePasswordChange=(e)=>{
        setFormData((prevState) => ({
            ...prevState,
            [e.target.name]: e.target.value,
        }));
        if (!passwordRegex.test(e.target.value)) {
            //console.log(passwordRegex.test("Kakarot1#"));
            setPasswordValidation('Password must be at least 8 characters long and contain at least one lowercase letter, one uppercase letter, one digit, and one special character.');
          } else {
            setPasswordValidation('');
          }
          if(e.target.value==''){
            setPasswordValidation('');

          }
    }
    const handleEmailChange = async (e) => {
        setFormData((prevState) => ({
            ...prevState,
            [e.target.name]: e.target.value,
        }));
        if (e.target.value.includes('@')) {

            const response = await organiserServices().checkEmail(e.target.value);
            if (response.isEmailTaken) {
                setEmailError("Email already exists");
            }
            else {
                setEmailError("");
            }
        }
    }
    const handleOrgInputChange = (e) => {
        setOrgFormData((prevState) => ({
            ...prevState,
            [e.target.name]: e.target.value,
        }));
    };
    const handleOrgNameChange = async (e) => {
        setOrgFormData((prevState) => ({
            ...prevState,
            [e.target.name]: e.target.value,
        }));
        if (e.target.value != "") {
            const isNameInOrganisation = await organisationServices().IsOrgNameTaken(e.target.value);
            if (isNameInOrganisation) {
                setOrgNameError("Organisation already exixts")
            }
            else {

                setOrgNameError('')
            }
        }
    }
    const handleRoleChange = (event) => {
        setSelectedRoleValue(event.target.value);

    };



    const handleSubmit = async (e) => {
        e.preventDefault();
        
        

      
        //console.log("before post call " + formData)
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
            _formData.append("organisationId", selectedOption);
            _formData.append("isActive", true);
            _formData.append("password", formData.Password);
            _formData.append("imgBody", formData.ImgBody);
            //console.log(_formData);
            const data = await organiserServices().registerPeer(_formData);
            //console.log("after call " + data)
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

            //console.log(_formData);
            const data = await organiserServices().addOwner(_formData);
            //console.log("after call " + data)


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


            IsActive: true,
            Password: "",
        })
    toast.success('Registered Succussfully!');

        navigate("/organiser/login");
    
    };

    return (
        <>
            <Container component="main" maxWidth="xl" className={classes.container}>
                <Box
                    sx={{  textAlign: "center" }}
                >
                    <div style={{ display: 'flex', justifyContent: 'center' }}>
                        <Typography variant="h5" component="h1" sx={{ width: "90%",maxWidth:'800px',color:'#3f50b5' }} >
                            Register
                        </Typography>
                    </div>
                    <div style={{ display: 'flex', justifyContent: 'center' }}>
                        <form className={classes.form} onSubmit={handleSubmit}>
                            <FormControl variant="outlined" sx={{marginTop:'10px'}} fullWidth>

                                <TextField
                                    label="Name"
                                    id="Name"
                                    name="AdministratorName"
                                    variant="outlined"
                                    value={formData.AdministratorName}
                                    onChange={handleInputChange}
                                    sx={{marginTop:'10px'}}
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
                                sx={{marginTop:'10px'}}
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
                                error={emailError !== ''}
                                helperText={emailError}
                                sx={{marginTop:'10px'}}
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
                                sx={{marginTop:'10px'}}
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
                                onChange={handlePasswordChange}
                                sx={{marginTop:'10px'}}
                                error={passwordValidation !== ''}
                                helperText={passwordValidation}
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
                                sx={{marginTop:'10px'}}
                                required
                                fullWidth
                            />

                            <input
                                accept="image/jpeg"
                                id="image-upload"
                                type="file"
                                onChange={handleImageChange}
                                style={{ display: 'none' }}
                                required
                               
                            />
                            <label htmlFor="image-upload" >
                                <Button sx={{marginTop:'10px',background:"#3f50b5"}} component="span" variant="contained"  fullWidth>
                                    Upload Image
                                </Button>
                            </label>
                            <div style={{ marginTop: "10px", }}>
                                {image && <p>Selected image: {image.name}</p>}
                                {imageError && <p>{imageError}</p>}
                            </div>


                            <FormControl required sx={{ width: '100%',maxWidth:'800px' }}>
                                <InputLabel htmlFor="role-select">Select an option</InputLabel>
                                <Select value={selectedRoleValue} labelId="role-select" id="role-select" onChange={handleRoleChange} >

                                    <MenuItem value="dummy">Select an Option</MenuItem>
                                    <MenuItem value="peer">Join an existing Organisation</MenuItem>
                                    <MenuItem value="owner">Create a new Organisation</MenuItem>

                                </Select>
                            </FormControl>
                            {selectedRoleValue == "owner" ? (<>
                                {/* <Typography variant="h5" gutterBottom sx={{marginTop:'20px',color:'#3f50b5'}}>
                                    Please fill in your organization details
                                </Typography> */}
                                <FormControl variant="outlined" sx={{marginTop:'10px'}} fullWidth>

                                    <TextField
                                        label="Name of the Organisation"
                                        id="Name"
                                        name="OrganisationName"
                                        variant="outlined"
                                        value={orgFormData.OrganisationName}
                                        onChange={handleOrgNameChange}
                                        error={orgNameError !== ''}
                                        helperText={orgNameError}
                                        required
                                        fullWidth
                                    />
                                </FormControl>
                                <FormControl variant="outlined" sx={{marginTop:'10px'}} fullWidth>
                                    <TextField
                                        label="Description"
                                        id="Description"
                                        name="OrganisationDescription"
                                        variant="outlined"
                                        value={orgFormData.OrganisationDescription}
                                        onChange={handleOrgInputChange}
                                        required
                                        multiline
                                        rows={4}
                                        fullWidth
                                    />
                                </FormControl>
                                <FormControl variant="outlined" sx={{marginTop:'10px'}} fullWidth>

                                    <TextField
                                        label="Location of the Organisation"
                                        id="Name"
                                        name="Location"
                                        variant="outlined"
                                        value={orgFormData.Location}
                                        onChange={handleOrgInputChange}
                                        required
                                        fullWidth
                                    />
                                </FormControl>
                            </>) : (selectedRoleValue == "peer" ? (<>
                               
                                <FormControl variant="outlined" sx={{marginTop:'10px'}} fullWidth>

                                    <TextField
                                        label="Enter your Organisation name"
                                        id="Name"
                                        name="Organisation"
                                        variant="outlined"

                                        onChange={handleOptionChange}
                                        error={peerOrgNameError !== ''}
                                        helperText={peerOrgNameError}
                                        sx={{marginTop:'10px'}}
                                        required
                                        fullWidth
                                    />
                                </FormControl>

                            </>

                            ) : <></>
                            )}

                            <Button type="submit" variant="contained" sx={{ background:'#3f50b5',marginTop:"10px" }} className={classes.submitButton}  fullWidth>
                              {selectedRoleValue == "peer"?"Register":"Submit Organisation request & Register"}
                            </Button>
                        </form>
                    </div>
                </Box>
              
            </Container>
        </>
    );
};

export default RegisterOrganiser;
