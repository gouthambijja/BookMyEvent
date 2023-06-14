import React, { useState } from "react";
import { makeStyles } from "@material-ui/core/styles";
import { loginAdminThunk } from "../Features/ReducerSlices/authSlice";
import {
  Container,
  Typography,
  TextField,
  Button,
  FormControl,
  InputLabel,
  Input,
  InputAdornment,
  IconButton,
} from "@material-ui/core";
import { Visibility, VisibilityOff } from "@material-ui/icons";
import { Box, Modal } from "@mui/material";
import { useLocation, useNavigate } from "react-router-dom";
import { useDispatch, useSelector } from "react-redux";
import { setLoading } from "../Features/ReducerSlices/loadingSlice";
import { setPersist } from "../Hooks/usePersist";
import { getAdminById } from "../Services/AdminServices";
import { getAdminByIdThunk } from "../Features/ReducerSlices/AdminSlice";
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

const Login = () => {
  const navigate = useNavigate();
  const location = useLocation();
  const dispatch = useDispatch();
  let from;
  if (location.pathname == "/admin/login")
    from = location.state?.pathname || "/admin";
  else from = location.state?.pathname || "/";
  const classes = useStyles();
  const [showPassword, setShowPassword] = useState(false);
  const [errMsg, setErrMsg] = useState({ open: false, msg: "" });
  const [formData, setFormData] = useState({
    email: "",
    password: "",
  });
  const isloading = useSelector((state) => state.isLoading);
  const auth = useSelector((state) => state.auth);
  const handleInputChange = (e) => {
    setFormData((prevState) => ({
      ...prevState,
      [e.target.name]: e.target.value,
    }));
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    dispatch(setLoading(true));
    try {
      console.log("hey");
      await dispatch(loginAdminThunk(formData)).unwrap();
      // await dispatch(getAdminByIdThunk(auth.id)).unwrap();
      setPersist();
      navigate(from);
    } catch {
      setErrMsg({ open: true, msg: "Login Failed, please try again." });
    }
    dispatch(setLoading(false));
  };

  const handleTogglePasswordVisibility = () => {
    setShowPassword((prevShowPassword) => !prevShowPassword);
  };

  return (
    <>
      <Container component="main" maxWidth="xl" className={classes.container}>
        <Box
          sx={{ boxShadow: 3, p: 3, borderRadius: "16px", textAlign: "center" }}
        >
          <Typography variant="h5" component="h1" border={1}>
            Admin Login
          </Typography>
          <form className={classes.form} onSubmit={handleSubmit}>
            <FormControl variant="outlined" margin="normal" fullWidth>
              <InputLabel htmlFor="Email Address">Email Address</InputLabel>
              <Input
                id="email"
                name="email"
                type="email"
                value={formData.email}
                onChange={handleInputChange}
                label="Email Address"
                required
              />
            </FormControl>
            <FormControl variant="outlined" margin="normal" fullWidth>
              <InputLabel htmlFor="password">Password</InputLabel>
              <Input
                id="password"
                name="password"
                type={showPassword ? "text" : "password"}
                value={formData.password}
                onChange={handleInputChange}
                required
                endAdornment={
                  <InputAdornment position="end">
                    <IconButton onClick={handleTogglePasswordVisibility}>
                      {showPassword ? <Visibility /> : <VisibilityOff />}
                    </IconButton>
                  </InputAdornment>
                }
                label="Password"
              />
            </FormControl>
            <Button
              type="submit"
              fullWidth
              variant="contained"
              color="primary"
              className={classes.submitButton}
            >
              Sign In
            </Button>
            {isloading ? <div>hey</div> : <div>oops</div>}
          </form>
        </Box>
        <Modal
          open={errMsg.open}
          onClose={() => {
            setErrMsg({ open: false, msg: "" });
          }}
          aria-labelledby="modal-modal-title"
          aria-describedby="modal-modal-description"
        >
          <Box sx={style}>
            <Typography id="modal-modal-title" variant="h6" component="h2">
              {errMsg.msg}
            </Typography>
          </Box>
        </Modal>
      </Container>
    </>
  );
};

export default Login;
