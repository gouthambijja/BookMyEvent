import React, { useEffect, useState } from "react";
import {
  TextField,
  Button,
  Typography,
  Container,
  FormControl,
  Select,
  MenuItem,
  InputLabel,
  Autocomplete,
} from "@mui/material";
import { useSelector } from "react-redux";
import { makeStyles } from "@material-ui/core/styles";
import store from "../App/store";
import { Box, Modal } from "@mui/material";
import org from "../Services/OrganisationService";
import { width } from "@mui/system";
import userServices from "../Services/UserServices";
import { useNavigate } from "react-router-dom";
import { toast } from "react-toastify";
import { GoogleLogin } from "@react-oauth/google";
import jwtDecode from "jwt-decode";

const useStyles = makeStyles((theme) => ({
  container: {
    display: "flex",
    flexDirection: "column",
    alignItems: "center",
    justifyContent: "center",
  },
  form: {
    width: "90%",
    maxWidth: "800px",
    marginTop: theme.spacing(1),
    display: "flex",
    flexDirection: "column",
    alignItems: "center",
    justifyContent: "center",
  },
  submitButton: {
    margin: theme.spacing(3, 0, 2),
  },
}));

const RegisterUser = () => {
  const classes = useStyles();
  // const admin = store.getState().admin.profile;
  const [image, setImage] = useState(null);
  const [passwordError, setPasswordError] = useState("");
  const [emailError, setEmailError] = useState("");
  const [selectedOption, setSelectedOption] = useState("");
  const [confirmPassword, setConfirmPassword] = useState("");
  const [passwordValidation, setPasswordValidation] = useState("");
  const [imageError, setImageError] = useState("");
  const navigate = useNavigate();
  const [formData, setFormData] = useState({
    Name: "",
    Email: "",
    PhoneNumber: "",
    UserAddress: "",
    CreatedOn: new Date().toLocaleString(),
    UpdatedOn: new Date().toLocaleString(),
    IsActive: true,
    ImageName: "profile",
    Password: "",
  });
  const passwordRegex =
    /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$/;

  const handleImageChange = async (e) => {
    const file = e.target.files[0];
    setImage(file);
    setFormData((prevState) => ({
      ...prevState,
      ImgBody: file,
    }));
    if (file) {
      // Perform image validations here
      const allowedTypes = ["image/jpeg", "image/png", "image/jpg"];

      if (!allowedTypes.includes(file.type)) {
        setImageError(
          "Invalid file type. Please select a JPEG, PNG or JPG image."
        );
      } else {
        setImageError("");
      }
    }
  };
  const handleConfirmPassword = (e) => {
    setConfirmPassword(e.target.value);
    if (e.target.value !== formData.Password) {
      setPasswordError("Passwords do not match");
    } else {
      // Passwords match, perform form submission or further processing
      setPasswordError("");
      // ...
    }
  };
  const handlePasswordChange = (e) => {
    setFormData((prevState) => ({
      ...prevState,
      [e.target.name]: e.target.value,
    }));
    if (!passwordRegex.test(e.target.value)) {
      setPasswordValidation(
        "Password must be at least 8 characters long and contain at least one lowercase letter, one uppercase letter, one digit, and one special character."
      );
    } else {
      setPasswordValidation("");
    }
    if (e.target.value == "") {
      setPasswordValidation("");
    }
  };
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
    if (e.target.value.includes("@")) {
      const response = await userServices().isEmailTaken(e.target.value);
      if (response.isEmailTaken) {
        setEmailError("Email already exists");
      } else {
        setEmailError("");
      }
    }
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    //console.log("before post call " + formData);
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
    //console.log(_formData);
    const data = await userServices().registerUser(_formData);
    //console.log("after call " + data);
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
    });
    toast.success("Registered!");
    navigate("/login");
  };
  function byteArrayToHexString(byteArray) {
    return Array.from(byteArray, (byte) => {
      return ('0' + (byte & 0xff).toString(16)).slice(-2);
    }).join('');
  }
  async function hashPassword(password) {
    const encoder = new TextEncoder();
    const data = encoder.encode(password);
    const hashBuffer = await crypto.subtle.digest('SHA-256', data);
    const hashArray = Array.from(new Uint8Array(hashBuffer));
    const hashHex = byteArrayToHexString(hashArray);
    return hashHex;
  }
  const handleGoogleSignIn = async (credentialResponse) => {
    const response = await userServices().isEmailTaken(credentialResponse.email);
      if (response.isEmailTaken) {
        toast.error('Registration Failed! Email already present in the database.');
        return;
      } 
    const credentials = jwtDecode(credentialResponse.credential);
    //console.log(credentials);
    const hash = await hashPassword(credentials.sub);
    const formData = {
      GoogleId: hash,
      Name: credentials.name,
      Email: credentials.email,
      ImageName: credentials.picture,
    };
    //console.log(formData);
    const data = await userServices().GoogleSignUp(formData);
    if (data.Item1) {
      toast.success("Registration success!");
      navigate("/login");
    }
    else{
        toast.error("Registration failed")
    }
  };
  return (
    <>
      <Container component="main" maxWidth="xl" className={classes.container}>
        <Box sx={{ padding: "40px", textAlign: "center" }}>
          <Container>
            <div style={{ display: "flex", justifyContent: "center" }}>
              <form className={classes.form} onSubmit={handleSubmit}>
                <FormControl
                  variant="outlined"
                  sx={{ marginTop: "10px" }}
                  fullWidth
                >
                  <TextField
                    label="Name"
                    id="Name"
                    name="Name"
                    variant="outlined"
                    value={formData.Name}
                    onChange={handleInputChange}
                    required
                    fullWidth
                  />
                </FormControl>
                <TextField
                  label="Email"
                  id="Email"
                  name="Email"
                  variant="outlined"
                  type="email"
                  value={formData.Email}
                  onChange={handleEmailChange}
                  error={emailError !== ""}
                  helperText={emailError}
                  sx={{ marginTop: "10px" }}
                  required
                  fullWidth
                />

                <TextField
                  label="Phone Number:"
                  id="Phone Number"
                  name="PhoneNumber"
                  variant="outlined"
                  type="number"
                  value={formData.PhoneNumber}
                  onChange={handleInputChange}
                  sx={{ marginTop: "10px" }}
                  required
                  fullWidth
                />
                <TextField
                  label="Address"
                  name="UserAddress"
                  id="Address"
                  variant="outlined"
                  value={formData.UserAddress}
                  onChange={handleInputChange}
                  sx={{ marginTop: "10px" }}
                  required
                  fullWidth
                />

                <TextField
                  label="Password"
                  id="Password"
                  name="Password"
                  variant="outlined"
                  type="password"
                  value={formData.Password}
                  onChange={handlePasswordChange}
                  sx={{ marginTop: "10px" }}
                  error={passwordValidation !== ""}
                  helperText={passwordValidation}
                  required
                  fullWidth
                />

                <TextField
                  label="ConfirmPassword"
                  id="ConfirmPassword"
                  name="Password"
                  variant="outlined"
                  type="password"
                  error={passwordError !== ""}
                  helperText={passwordError}
                  value={confirmPassword}
                  onChange={handleConfirmPassword}
                  sx={{ marginTop: "10px" }}
                  required
                  fullWidth
                />

                <input
                  accept="image/jpeg"
                  id="image-upload"
                  type="file"
                  onChange={handleImageChange}
                  style={{ display: "none" }}
                  required
                />
                <label
                  style={{ width: "100%", maxWidth: "800px" }}
                  htmlFor="image-upload"
                >
                  <Button
                    component="span"
                    variant="contained"
                    sx={{
                      marginTop: "10px",
                      width: "100%",
                      background: "#3f50b5",
                    }}
                  >
                    Upload Profile Picture
                  </Button>
                </label>
                {image && <p>Selected image: {image.name}</p>}
                {imageError && <p>{imageError}</p>}
                <Button
                  sx={{ marginTop: "10px", background: "#3f50b5" }}
                  type="submit"
                  variant="contained"
                  className={classes.submitButton}
                  color="primary"
                  fullWidth
                >
                  Register
                </Button>
              </form>
            </div>
            <div
              style={{
                display: "flex",
                justifyContent: "center",
                flexWrap: "wrap",
              }}
            >
              <div
                style={{
                  flexBasis: "100%",
                  maxWidth: "800px",
                  margin: "20px 0px",
                }}
              >
                <div
                  style={{
                    position: "relative",
                    borderBottom: "1px solid #d0d0d0",
                  }}
                >
                  <span
                    style={{
                      background: "#fff",
                      padding: "0px 15px",
                      position: "absolute",
                      left: "50%",
                      transform: " translate(-50%,-50%)",
                      color: "#a0a0a0",
                    }}
                  >
                    or
                  </span>
                </div>
              </div>
              <div
                style={{
                  flexBasis: "100%",
                  maxWidth: "800px",
                  margin: "20px 0px",
                  display: "flex",
                  justifyContent: "center",
                }}
              >
                <GoogleLogin
                  onSuccess={(credentialResponse) => {
                    handleGoogleSignIn(credentialResponse);
                  }}
                  onError={() => {
                    toast.error("Sign up failed :(");
                  }}
                />
              </div>
            </div>
          </Container>
        </Box>
      </Container>
    </>
  );
};

export default RegisterUser;
