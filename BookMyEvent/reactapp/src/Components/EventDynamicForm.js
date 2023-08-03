import React, { useState } from "react";
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
} from "@mui/material";
import { styled } from "@mui/system";
import { useDispatch, useSelector } from "react-redux";
import {
  clearFormFieldsForm,
  setInputFields,
  setIsFree,
  unsetIsFree,
} from "../Features/ReducerSlices/EventRegistrationFormFieldsSlice";
import { useNavigate } from "react-router-dom";
import organisationFormServies from "../Services/OrganiserFormServices";
import { toast } from "react-toastify";
const EventDynamicForm = () => {
  const navigate = useNavigate();
  const [formNameErrorMsg, setFormNameErrorMsg] = useState("");
  const [FormName, setFormName] = useState("");
  const [FieldOption, SetFieldOption] = useState("");
  const FormFieldInput = { width: "100%", marginBottom: "10px" };
  const FormFields = useSelector((store) => store.formFields.formFields);
  const FileTypes = useSelector((store) => store.formFields.fileTypes);
  const isFree = useSelector(
    (store) => store.EventRegistrationFormFields.isFree
  );
  const [TicketObject, setTicketObject] = useState({
    FieldType: "Select",
    Label: "Ticket Prices",
    Validations: { min: "", max: "" },
    Options: [1000],
    IsRequired: false,
  });

  let count = 0;

  const inputFields = useSelector(
    (store) => store.EventRegistrationFormFields.inputFields
  );
  //   const [formNameErrorMsg,setFormNameErrorMsg] = useState("");
  const NewFieldObject = {
    FieldType: "",
    Label: "",
    Validations: {},
    Options: [],
    IsRequired: false,
  };

  const dispatch = useDispatch();
  const handleFormNameChange = async (event) => {
    const name = event.target.value;
    setFormName(name);
    if (name != "") {
      const res = await organisationFormServies().IsFormNameTaken(name);
      if (res) {
        setFormNameErrorMsg(
          "form name already available choose different name!"
        );
      } else {
        setFormNameErrorMsg("");
      }
    }
  };
  const handleInputChange = (event, index) => {
    if (index == 0 && !isFree) return;
    const { name, value, checked } = event.target;
    const values = [...inputFields];
    if (name == "Label") {
      if (value == "Ticket Prices") {
        toast.error(
          "'Ticket Prices' can't be a label, enter other valid label"
        );
        return;
      }
    }
    if (name == "FieldType") {
      values[index] = NewFieldObject;
    }

    if (name == "minValue") {
      values[index] = {
        ...values[index],
        Validations: { ...values[index].Validations, min: value },
      };
    } else if (name == "maxValue") {
      values[index] = {
        ...values[index],
        Validations: { ...values[index].Validations, max: value },
      };
    } else {
      values[index] = {
        ...values[index],
        [name]: name == "IsRequired" ? checked : value,
      };
    }
    //console.log(values);
    dispatch(setInputFields(values));
  };
  const handleAddFields = () => {
    dispatch(setInputFields([...inputFields, NewFieldObject]));
  };
  const handleAddTicketPrices = () => {
    dispatch(unsetIsFree());
    dispatch(setInputFields([TicketObject, ...inputFields]));
  };
  const handleRemoveTicketPrices = () => {
    dispatch(setIsFree());
    const temp = [...inputFields];
    temp.splice(0, 1);
    dispatch(setInputFields([...temp]));
  };
  const ColorfulButton = styled(Button)({
    backgroundColor: "#3f50b5",
    color: "#fff",
    "&:hover": {
      backgroundColor: "#d81b60",
    },
  });
  const handleOptionDelete = (value, index) => {
    const values = [...inputFields];
    let Field = values[index];
    let options = [...Field.Options];
    options.splice(options.indexOf(value), 1);
    Field = { ...Field, Options: [...options] };
    values[index] = { ...Field };
    dispatch(setInputFields(values));
  };
  const handleAddFieldOption = (e, index) => {
    const values = [...inputFields];
    let Field = values[index];
    let options = Field.Options;
    options = [...options, FieldOption];
    Field = { ...Field, Options: [...options] };
    values[index] = { ...Field };
    //console.log(values);
    SetFieldOption("");
    dispatch(setInputFields(values));
  };
  const handleRemoveFields = (index) => {
    console.log("hey");
    if (
      inputFields.length != 0 &&
      inputFields[index].Label != "Ticket Prices"
    ) {
      const values = [...inputFields];
      values.splice(index, 1);
      dispatch(setInputFields(values));
    }
  };
  const handleSubmit = async (e) => {
    e.preventDefault();
    if (formNameErrorMsg == "") {
      if (inputFields.length == 0) {
        toast.warning("There should be atlest one Field");
        return;
      }
      try {
        const response = await organisationFormServies().AddForm(FormName);
        console.log(response);
        if (response) {
          dispatch(clearFormFieldsForm());
          toast.success("Form submission succes!");
          navigate("/organiser/addevent");
        }
      } catch (er) {
        toast.error(er);
        toast.error("Form Submission Failed");
      }
    }
  };
  const FormStyle = {
    display: "flex",
    flexDirection: "column",
    gap: "1rem",
    maxWidth: "800px",
    margin: "0 auto",
    padding: "1rem",
    backgroundColor: "#fff",
    borderRadius: "8px",
  };
  return (
    <form style={FormStyle} onSubmit={handleSubmit}>
      <Box
        sx={{
          border: "1px solid #d0d0d0",
          padding: "16px",
          borderRadius: "4px",
        }}
      >
        <TextField
          sx={{ marginBottom: "10px" }}
          label="Form Name"
          name="FormName"
          variant="outlined"
          type="text"
          value={FormName}
          onChange={handleFormNameChange}
          error={formNameErrorMsg !== ""}
          helperText={formNameErrorMsg}
          required
          fullWidth
        />
        <FormControlLabel
          sx={{ marginBottom: "10px" }}
          control={
            <Checkbox
              name="IsFree"
              checked={isFree}
              onChange={() => {
                if (isFree == false) {
                  handleRemoveTicketPrices();
                } else {
                  handleAddTicketPrices();
                }
              }}
            />
          }
          label="Is Free"
        />
        {inputFields.map((inputField, index) => (
          <div
            key={++count}
            style={{
              border: "1px solid #d0d0d0",
              padding: "20px",
              borderRadius: "4px",
              marginBottom: "10px",
            }}
          >
            <FormControl style={FormFieldInput}>
              <InputLabel>Select Field Type</InputLabel>
              <Select
                name="FieldType"
                value={
                  inputField.FieldType != undefined ? inputField.FieldType : ""
                }
                label="Select Field Type"
                onChange={(e) => handleInputChange(e, index)}
                required
                disabled={index == 0 && !isFree}
              >
                {FormFields.map((option) => (
                  <MenuItem key={++count} value={option.type}>
                    {option.type}
                  </MenuItem>
                ))}
              </Select>
            </FormControl>
            <TextField
              label="Field Label"
              name="Label"
              required
              value={inputField.Label}
              onChange={(event) => handleInputChange(event, index)}
              style={FormFieldInput}
              disabled={index == 0 && !isFree}
            />
            <FormControlLabel
              control={
                <Checkbox
                  name="IsRequired"
                  checked={inputField.IsRequired}
                  onChange={(e) => handleInputChange(e, index)}
                  disabled={index == 0 && !isFree}
                />
              }
              label="Is Required"
            />
            
            {inputField.FieldType == "File" ? (
              <FormControl style={{...FormFieldInput,marginTop:'10px'}}>
              <InputLabel>Select File Type</InputLabel>
             <Select
                name="FileType"
                value={
                  inputField.FileType != undefined ? inputField.FileType : ""
                }
                label="Select File Type"
                onChange={(e) => {console.log('hey');handleInputChange(e, index)}}
                required
                disabled={index == 0 && !isFree}
              >
                {FileTypes.map((option) => (
                  <MenuItem key={++count} value={option.fileTypeId}>
                    {option.fileTypeName}
                  </MenuItem>
                ))}
              </Select></FormControl>
            ) : (
              <></>
            )}
            {(inputField.FieldType == "Date" ||
              inputField.FieldType == "Number") &&
            inputField.FieldType != "" &&
            inputField.FieldType !== undefined ? (
              <>
                <TextField
                  type={inputField.FieldType}
                  name="minValue"
                  label={`Min/Start ${
                    inputField.Label ? inputField.Label : ""
                  }`}
                  value={inputField.Validations?.min}
                  onChange={(event) => handleInputChange(event, index)}
                  style={FormFieldInput}
                ></TextField>
                <TextField
                  type={inputField.FieldType}
                  name="maxValue"
                  label={`Max/End ${inputField.Label ? inputField.Label : ""}`}
                  value={inputField.Validations?.max}
                  onChange={(event) => handleInputChange(event, index)}
                  style={FormFieldInput}
                ></TextField>
              </>
            ) : (inputField.FieldType == "Select" ||
                inputField.FieldType == "Radio" ||
                inputField.FieldType == "CheckBox") &&
              inputField.FieldType != "" &&
              inputField.FieldType !== undefined ? (
              <Box
                style={FormFieldInput}
                sx={{
                  padding: "16px",
                  border: "1px solid #d0d0d0",
                  borderRadius: "4px",
                }}
              >
                {inputField.Options.map((option) => (
                  <Chip
                    key={++count}
                    sx={{
                      width: "100%",
                      display: "flex",
                      justifyContent: "space-between",
                      marginBottom: "10px",
                    }}
                    label={option}
                    name={option}
                    variant="outlined"
                    onDelete={() => handleOptionDelete(option, index)}
                  />
                ))}
                <TextField
                  type={index == 0 && !isFree ? "number" : "text"}
                  style={FormFieldInput}
                  label={
                    index == 0 && !isFree ? "Add ticket price" : "Enter Option"
                  }
                  value={FieldOption}
                  onChange={(e) => {
                    SetFieldOption(e.target.value);
                  }}
                ></TextField>
                <ColorfulButton
                  type="button"
                  variant="contained"
                  fullWidth
                  onClick={(e) => {
                    if (FieldOption !== "") handleAddFieldOption(e, index);
                  }}
                >
                  Add option for {inputField.FieldType} Field
                </ColorfulButton>
              </Box>
            ) : (
              <></>
            )}

            <Button
              variant="outlined"
              onClick={() => handleRemoveFields(index)}
            >
              Remove
            </Button>
          </div>
        ))}
        <ColorfulButton
          type="button"
          variant="contained"
          style={{ marginBottom: "10px" }}
          fullWidth
          onClick={handleAddFields}
        >
          Add Form Field
        </ColorfulButton>
      </Box>
      <ColorfulButton type="submit" variant="contained" fullWidth>
        Submit Form
      </ColorfulButton>
    </form>
  );
};

export default EventDynamicForm;
