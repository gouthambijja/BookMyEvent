import ViewForm from "./ViewForm";
import React, { useEffect, useState } from "react";
import { Country, State, City } from "country-state-city";
import {
  TextField,
  Button,
  FormControlLabel,
  Checkbox,
  Typography,
  FormControl,
  InputLabel,
  Select,
  MenuItem,
  Box,
  Chip,
  ButtonBase,
} from "@mui/material";
import { styled } from "@mui/system";
import { useDispatch, useSelector } from "react-redux";
import EventDynamicForm from "./EventDynamicForm";
import {
  resetEventData,
  setEventData,
} from "../Features/ReducerSlices/EventRegistrationFormSlice";
import { useNavigate } from "react-router-dom";
import store from "../App/store";
import { fetchOrganiserForms } from "../Features/ReducerSlices/OrganiserFormsSlice";
import EventServices from "../Services/EventServices";
import { createEvent } from "../Features/ReducerSlices/EventsSlice";
import { AddRegisteredEventId } from "../Features/ReducerSlices/HomeEventsSlice";
import Modal from "@mui/material/Modal";
import OrganiserFormServices from "../Services/OrganiserFormServices";
const ColorfulForm = styled("form")({
  display: "flex",
  flexDirection: "column",
  gap: "1rem",
  maxWidth: "800px",
  margin: "0 auto",
  padding: "1rem",
  backgroundColor: "#fff",
  borderRadius: "8px",
});

const ColorfulButton = styled(Button)({
  backgroundColor: "#3f50b5",
  color: "#fff",
  "&:hover": {
    backgroundColor: "#d81b60",
  },
});
const style = {
  position: "absolute",
  top: "50%",
  left: "50%",
  transform: "translate(-50%, -50%)",
  width: "90%",
  maxWidth: "800px",
  backgroundColor: "#fff",
  borderRadius: "4px",
  boxShadow: 24,
  boxShadow: "0px 0px 15px rgb(0,0,0,0.6)",
  p: 4,
};

let count = 1;

const EventForm = () => {
  const dispatch = useDispatch();
  const navigate = useNavigate();
  const auth = useSelector((store) => store.auth);
  const OrganiserInfo = useSelector((store) => store.profile.info);
  const organisationForms = useSelector((store) => store.orgForms.forms);
  const categoryOptions = useSelector((store) => store.category.categories);
  const FormFields = useSelector((store) => store.formFields.formFields);
  const countryOptions = Country.getAllCountries();
  const [stateOptions, setStateOptions] = useState([]);
  const [cityOptions, setCityOptions] = useState([]);
  const [countryIsoCode, setCountryIsoCode] = useState("");
  const [Form, SetForm] = useState("");
  const eventData = useSelector(
    (store) => store.EventRegistrationForm.eventData
  );

  const [images, setImages] = useState([]);
  const [profileImage, setProfileImage] = useState();
  const [modalData, setModalData] = useState({ open: false, formId: "" });
  const handleOpen = (e, formId) =>
    setModalData({ open: true, formId: formId });
  const handleClose = () => setModalData({ open: false, formId: "" });
  const handleImageChange = async (e) => {
    const file = e.target.files[0];
    setImages((prev) => [...prev, file]);
  };
  const handleProfileImage = async (e) => {
    const file = e.target.files[0];
    setProfileImage(file);
    //console.log(profileImage);
  };
  const handleSubmit = async (e) => {
    e.preventDefault();
    //console.log(eventData);
    //console.log(profileImage);
    //console.log(images);
    const formFields = await OrganiserFormServices().getFieldTypesByFormId(
      eventData.FormId
    );
    let ticketPrices = formFields.find(e=> e.lable == "Ticket Prices")?.options;
    let min=0,max =0,isFree = false;
    if(ticketPrices){
      ticketPrices = JSON.parse(ticketPrices);
      min = Number(ticketPrices[0]);
      max = min;
      for(let i=1;i<ticketPrices.length;i++){
        min = Math.min(min,Number(ticketPrices[i]));
        max = Math.max(max,Number(ticketPrices[i]));
      }
    }
    if(min == 0 && max == 0 ){
      isFree = true;
    }
    const _formData = new FormData();
    _formData.append("EventName", eventData.EventName);
    _formData.append("StartDate", eventData.StartDate);
    _formData.append("EndDate", eventData.EndDate);
    _formData.append("CategoryId", eventData.categoryId);
    _formData.append("Capacity", eventData.Capacity);
    _formData.append("AvailableSeats", eventData.Capacity);
    _formData.append("ImgBody", profileImage);
    _formData.append("Description", eventData.Description);
    _formData.append("Location", eventData.Location);
    _formData.append("Country", eventData.Country);
    _formData.append("State", eventData.State);
    _formData.append("City", eventData.City);
    if (auth.role == "Owner" || auth.role == "Secondary_Owner")
      _formData.append("IsPublished", false);
    else _formData.append("IsPublished", false);
    _formData.append("IsCancelled", false);
    _formData.append(
      "MaxNoOfTicketsPerTransaction",
      eventData.MaxNoOfTicketsPerTransaction
    );
    _formData.append("EventStartingPrice",min);
    _formData.append("EventEndingPrice",max);
    _formData.append("CreatedOn", new Date().toLocaleString());
    _formData.append("UpdatedBy", auth.id);
    _formData.append("UpdatedOn", new Date().toLocaleString());
    _formData.append("IsFree", isFree);
    _formData.append("IsActive", true);
    _formData.append("OrganisationId", OrganiserInfo.organisationId);
    _formData.append("FormId", eventData.FormId);
    _formData.append("RegistrationStatusId", 1);
    _formData.append("CreatedBy", auth.id);
    if (auth.role == "Owner" || auth.role == "Secondary_Owner")
      _formData.append("AcceptedBy", auth.id);
    for (let i = 0; i < images.length; i++) {
      _formData.append(`CoverImage${i}`, images[i]);
    }
    try {
      var resp = await dispatch(createEvent(_formData)).unwrap();
      //console.log(resp);

      dispatch(resetEventData());
      navigate("/organiser");
    } catch {
      //console.log("event Form Registration Failed");
    }
    // const res = await EventServices.addNewEvent(_formData);
  };

  const handleChange = async (e) => {
    let { name, value, type, checked } = e.target;
    if (e.target.name == "EventCategory") {
      SetEventCategory(value);
      let categoryId = categoryOptions.find(
        (e) => e.categoryName == value
      ).categoryId;
      name = "categoryId";
      value = categoryId;
    } else if (e.target.name == "Form") {
      SetForm(value);
      name = "FormId";
      value = organisationForms.find((e) => e.formName == value).formId;
    } else if (e.target.name == "Country") {
      setSelectedCountry(value);
      const isoCode = countryOptions.find((e) => e.name == value).isoCode;
      setCountryIsoCode(isoCode);
      setStateOptions(State.getStatesOfCountry(isoCode));
      setCityOptions([]);
      setSelectedCity("");
      setSelectedState("");
    } else if (e.target.name == "State") {
      setSelectedState(value);
      setCityOptions(
        City.getCitiesOfState(
          countryIsoCode,
          stateOptions.find((e) => e.name == value).isoCode
        )
      );
      setSelectedCity("");
    } else if (e.target.name == "City") {
      setSelectedCity(value);
    }
    dispatch(setEventData({ checked, value, name, type }));
    //console.log(eventData);
  };
  const [EventCategory, SetEventCategory] = useState("");
  const [SelectedCountry, setSelectedCountry] = useState("");
  const [SelectedState, setSelectedState] = useState("");
  const [SelectedCity, setSelectedCity] = useState("");
  useEffect(() => {
    let isMounted = true;
    const loadForms = async () => {
      await store
        .dispatch(
          fetchOrganiserForms(store.getState().profile.info.organisationId)
        )
        .unwrap();
      return null;
    };
    loadForms();
    return () => {
      isMounted = false;
      loadForms();
    };
  }, []);
  return (
    <ColorfulForm onSubmit={handleSubmit}>
      <Typography variant="h5" align="center" color="primary">
        Event Details
      </Typography>
      <TextField
        label="Event Name"
        name="EventName"
        value={eventData.EventName}
        onChange={handleChange}
        fullWidth
        required
      />
      <Box
        sx={{
          border: "1px solid #d0d0d0",
          borderRadius: "4px",
          padding: "20px",
        }}
      >
        <ColorfulButton
          type="button"
          sx={{ marginBottom: "10px" }}
          variant="contained"
          fullWidth
          onClick={() => {
            navigate("/organiser/createNewEventRegistrationForm");
          }}
        >
          Create New Form
        </ColorfulButton>
        <hr></hr>
        <Box sx={{ fontSize: "1.2rem", marginBottom: "10px" }}>
          {" "}
          Select Registration Form For Users
        </Box>
        <Box>
          <FormControl fullWidth>
            <InputLabel>Select Form</InputLabel>
            <Select required name="Form" value={Form} onChange={handleChange}>
              {organisationForms.map((option, index) => (
                <MenuItem key={++count} value={option.formName}>
                  <div style={{ postion: "relative", padding: "0px 20px" }}>
                    {option.formName}
                    <button
                      onClick={(e) => {
                        e.stopPropagation();
                        handleOpen(e, option.formId);
                      }}
                      style={{
                        position: "absolute",
                        right: "30px",
                        border: "none",
                        color: "#fff",
                        background: "#3f50b5",
                        outline: "none",
                        borderRadius: "4px",
                        padding: "4px",
                      }}
                    >
                      view
                    </button>
                  </div>
                </MenuItem>
              ))}
            </Select>
          </FormControl>
        </Box>
      </Box>
      <TextField
        label="Start Date"
        name="StartDate"
        type="date"
        value={eventData.StartDate}
        onChange={handleChange}
        fullWidth
        required
        InputLabelProps={{ shrink: true }}
      />
      <TextField
        label="End Date"
        name="EndDate"
        type="date"
        value={eventData.EndDate}
        onChange={handleChange}
        fullWidth
        required
        InputLabelProps={{ shrink: true }}
      />
      <FormControl fullWidth>
        <InputLabel>Select Category</InputLabel>
        <Select
          required
          name="EventCategory"
          value={EventCategory}
          onChange={handleChange}
        >
          {categoryOptions.map((option, index) => (
            <MenuItem key={++count} value={option.categoryName}>
              {option.categoryName}
            </MenuItem>
          ))}
        </Select>
      </FormControl>
      <TextField
        label="Capacity"
        name="Capacity"
        type="number"
        inputProps={{ min: 1 }}
        value={eventData.Capacity}
        onChange={handleChange}
        fullWidth
        required
      />
      <TextField
        label="Description"
        name="Description"
        value={eventData.Description}
        onChange={handleChange}
        fullWidth
        multiline
        required
        rows={4}
      />
      <FormControl fullWidth>
        <InputLabel>Select Country</InputLabel>
        <Select
          required
          name="Country"
          value={SelectedCountry}
          onChange={handleChange}
        >
          {countryOptions.map((option, index) => (
            <MenuItem key={++count} value={option.name}>
              {option.name}
            </MenuItem>
          ))}
        </Select>
      </FormControl>
      <FormControl fullWidth>
        <InputLabel>Select State</InputLabel>
        <Select
          required
          name="State"
          value={SelectedState}
          onChange={handleChange}
        >
          {stateOptions.map((option, index) => (
            <MenuItem key={++count} value={option.name}>
              {option.name}
            </MenuItem>
          ))}
        </Select>
      </FormControl>
      <FormControl fullWidth>
        <InputLabel>Select City</InputLabel>
        <Select
          required
          name="City"
          value={SelectedCity}
          onChange={handleChange}
        >
          {cityOptions.map((option, index) => (
            <MenuItem key={++count} value={option.name}>
              {option.name}
            </MenuItem>
          ))}
        </Select>
      </FormControl>
      <TextField
        label="Location"
        name="Location"
        value={eventData.Location}
        onChange={handleChange}
        fullWidth
        required
      />
      <TextField
        label="Max No. of Tickets per Transaction"
        name="MaxNoOfTicketsPerTransaction"
        type="number"
        inputProps={{ min: 1 }}
        value={eventData.MaxNoOfTicketsPerTransaction}
        onChange={handleChange}
        fullWidth
        required
      />
      <Box
        sx={{
          border: "1px solid #d0d0d0",
          borderRadius: "4px",
          padding: "16px",
        }}
      >
        <div>profile Image*</div>
        <hr></hr>
        <input
          required
          type="file"
          name="ProfileImage"
          onChange={handleProfileImage}
        />
      </Box>
      <Box
        sx={{
          border: "1px solid #d0d0d0",
          borderRadius: "4px",
          padding: "16px",
        }}
      >
        <div>Cover Images</div>
        <hr></hr>
        <input type="file" name="CoverImage1" onChange={handleImageChange} />
        <input type="file" name="CoverImage2" onChange={handleImageChange} />
        <input type="file" name="CoverImage3" onChange={handleImageChange} />
        <input type="file" name="CoverImage4" onChange={handleImageChange} />
        <input type="file" name="CoverImage5" onChange={handleImageChange} />
        <input type="file" name="CoverImage6" onChange={handleImageChange} />
      </Box>

      <ColorfulButton type="submit" variant="contained" fullWidth>
        Submit
      </ColorfulButton>
      <Modal
        open={modalData.open}
        onClose={handleClose}
        aria-labelledby="modal-modal-title"
        aria-describedby="modal-modal-description"
      >
        <Box sx={style}>
          <Box
            className="customScrollbar"
            sx={{ maxHeight: "90vh", overflow: "auto" }}
          >
            <ViewForm formId={modalData.formId} />
          </Box>
        </Box>
      </Modal>
    </ColorfulForm>
  );
};

export default EventForm;
