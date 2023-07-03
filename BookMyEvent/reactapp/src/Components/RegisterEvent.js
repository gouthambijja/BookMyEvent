import React, { useEffect, useState } from "react";
import {
  TextField,
  Select,
  MenuItem,
  FormControl,
  FormLabel,
  RadioGroup,
  FormControlLabel,
  Radio,
  Button,
} from "@mui/material";
import OrganiserFormServices from "../Services/OrganiserFormServices";
import { useNavigate, useParams } from "react-router-dom";
import { useDispatch, useSelector } from "react-redux";
import store from "../App/store";
import EventServices from "../Services/EventServices";
import UserInputFormServices from "../Services/UserInputFormServices";
import Transactions from "./Transactions";
import { toast } from "react-toastify";
import { AddRegisteredEventId } from "../Features/ReducerSlices/HomeEventsSlice";

const RegisterEvent = () => {
  const [eventRegistrationFormFields, setEventRegistrationFormFields] =
    useState([]);
  const [event, setEvent] = useState();
  const [toggleRegistrationTransaction, setToggleRegistrationTransaction] =
    useState(true);
  const [RegisteredData, setRegisteredData] = useState();
  const navigate = useNavigate();
  let fetchedFormFields = [];
  const { eventId, formId } = useParams();
  const dispatch= useDispatch();
  const [TotalPrice, setTotalPrice] = useState(0);
  const FormFieldTypes = useSelector((store) => store.formFields.formFields);
  const [fieldRegistrationId, setFieldRegistrationId] = useState();
  useEffect(() => {
    const loadUserEventRegistrationFormFields = async () => {
      const event = await EventServices().getEventById(eventId);
      setEvent(event);
      console.log(event);
      const formFields = await OrganiserFormServices().getFieldTypesByFormId(
        formId
      );
      formFields.forEach((e) => {
        setFieldRegistrationId((prev) => {
          return { ...prev, [e.lable]: e.registrationFormFieldId };
        });
      });
      setEventRegistrationFormFields([
        ...eventRegistrationFormFields,
        [...formFields],
      ]);
      fetchedFormFields = [...formFields];
      console.log(fetchedFormFields);
    };
    loadUserEventRegistrationFormFields();
    return () => {
      loadUserEventRegistrationFormFields();
    };
  }, []);
  const [formData, setFormData] = useState([]);

  const handlechange = (event, index, label) => {
    const { name, value } = event.target;
    setFormData((prevData) => {
      prevData[index] = { ...prevData[index], [name]: value };
      console.log(prevData);
      return prevData;
    });
    console.log(formData);
  };

  const handleSubmit = async (e) => {
    console.log(fieldRegistrationId);
    e.preventDefault();
    let formResult = [];
    formData.forEach((e, index) => {
      let formFieldResponse = [];
      for (let i in e) {
        const type = eventRegistrationFormFields[0].find((u) => {
          return u.lable == i;
        }).fieldTypeId;

        if (type == 1 || type == 3 || type == 6 || type == 5) {
          formFieldResponse.push({
            label: i,
            stringResponse: `${e[i]}`,
            registrationFormFieldId: fieldRegistrationId[i],
          });
          if (i == "Ticket Prices") {
            setTotalPrice((prev) => prev + Number(e[i]));
            console.log(TotalPrice);
          }
        } else if (type == 2) {
          formFieldResponse.push({
            label: i,
            numberResponse: e[i],
            registrationFormFieldId: fieldRegistrationId[i],
          });
        } else if (type == 4) {
          const date = new Date(e[i]);
          formFieldResponse.push({
            label: i,
            dateResponse: date,
            registrationFormFieldId: fieldRegistrationId[i],
          });
        }
      }
      formResult.push(formFieldResponse);
    });
    console.log(formResult);
    console.log(formData);
    let formResultPost = [];
    for (let i = 0; i < eventRegistrationFormFields.length; i++) {
      formResultPost.push({
        ["userInputFormBL"]: {
          // ["userInputFormId"]: "3fa85f64-5717-4562-b3fc-2c963f66afa6",
          ["userId"]: store.getState().auth.id,
          ["eventId"]: event.eventId,
        },
        ["userInputFormFields"]: formResult[i],
      });
    }
    console.log(formResultPost);
    setRegisteredData(
      await UserInputFormServices().submitUserInputForm(formResultPost)
    );
    dispatch(AddRegisteredEventId(event.eventId));
    setToggleRegistrationTransaction(false);
  };
  const handleAddPerson = () => {
    if (
      eventRegistrationFormFields.length < event.maxNoOfTicketsPerTransaction
    ) {
      setEventRegistrationFormFields([
        ...eventRegistrationFormFields,
        [...eventRegistrationFormFields[0]],
      ]);
      console.log(eventRegistrationFormFields);
    } else {
      toast.warning(`Per transaction you can register max of ${event.maxNoOfTicketsPerTransaction} tickets only`)
    }
  };
  const handleRemove = (index) => {
    if (eventRegistrationFormFields.length > 1) {
      const copyArray = [...eventRegistrationFormFields];
      copyArray.splice(index, 1);
      setEventRegistrationFormFields(copyArray);
    }
  };
  const formContainerStyle = {
    margin: "0px auto",
    display: "flex",
    width: "90%",
    maxWidth: "800px",
    flexDirection: "column",
    alignItems: "center",
    padding: "20px",
    backgroundColor: "#fff",
    borderRadius: "8px",
    transition: "transform 0.3s ease",
    "&:hover": {
      transform: "scale(1.02)",
    },
  };
  let cnt = 0;
  const submitButtonStyle = {
    marginTop: "20px",
    backgroundColor: "#4caf50",
    color: "#fff",
    fontWeight: "bold",
    borderRadius: "4px",
    padding: "12px 24px",
    cursor: "pointer",
    border: "none",
    boxShadow: "0 2px 4px rgba(0, 0, 0, 0.2)",
    transition: "background-color 0.3s ease",
    textTransform: "uppercase",
    letterSpacing: "1px",
    fontSize: "14px",
    "&:hover": {
      backgroundColor: "#45a049",
    },
  };
  return (
    <>
      {toggleRegistrationTransaction ? (
        <form style={formContainerStyle} onSubmit={handleSubmit}>
          {eventRegistrationFormFields.map((person, index) => (
            <div
              key={cnt++}
              style={{
                padding: "30px",
                width: "100%",
                maxWidth: "800px",
                border: "1px solid #d0d0d0",
                borderRadius: "4px",
                marginBottom: "20px",
              }}
            >
              {person.map((formField) => (
                <div key={cnt++}>
                  {FormFieldTypes[Number(formField.fieldTypeId) - 1].type ==
                  "Text" ? (
                    <TextField
                      style={{
                        marginBottom: "20px",
                        width: "100%",
                        maxWidth: "800px",
                      }}
                      type="text"
                      name={formField.lable}
                      label={formField.lable}
                      value={formData[formField.lable]}
                      onChange={(e) => {
                        handlechange(e, index, formField.lable);
                      }}
                      required
                    />
                  ) : FormFieldTypes[Number(formField.fieldTypeId) - 1].type ==
                    "Number" ? (
                    <TextField
                      style={{
                        marginBottom: "20px",
                        width: "100%",
                        maxWidth: "800px",
                      }}
                      type="number"
                      name={formField.lable}
                      label={formField.lable}
                      value={formData[formField.lable]}
                      onChange={(e) => {
                        handlechange(e, index);
                      }}
                      inputProps={{
                        min: Number(JSON.parse(formField.validations).min),
                        max: Number(JSON.parse(formField.validations).max),
                      }}
                      required
                    />
                  ) : FormFieldTypes[Number(formField.fieldTypeId) - 1].type ==
                    "Email" ? (
                    <TextField
                      style={{
                        marginBottom: "20px",
                        width: "100%",
                        maxWidth: "800px ",
                      }}
                      type="email"
                      name={formField.lable}
                      label={formField.lable}
                      value={formData[formField.lable]}
                      onChange={(e) => {
                        handlechange(e, index);
                      }}
                      required
                    />
                  ) : FormFieldTypes[Number(formField.fieldTypeId) - 1].type ==
                    "Date" ? (
                    <TextField
                      style={{
                        marginBottom: "20px",
                        width: "100%",
                        maxWidth: "800px ",
                      }}
                      type="date"
                      name={formField.lable}
                      label={formField.lable}
                      value={formData[formField.lable]}
                      onChange={(e) => {
                        handlechange(e, index);
                      }}
                      required
                    />
                  ) : FormFieldTypes[Number(formField.fieldTypeId) - 1].type ==
                    "Radio" ? (
                    <FormControl
                      style={{
                        marginBottom: "20px",
                        width: "100%",
                        maxWidth: "800px ",
                      }}
                    >
                      <FormLabel>{formField.lable}</FormLabel>
                      <RadioGroup
                        name={formField.lable}
                        value={formData[formField.lable]}
                        onChange={(e) => {
                          handlechange(e, index);
                        }}
                        required
                      >
                        {JSON.parse(formField.options).map((option) => (
                          <FormControlLabel
                            value={option}
                            control={<Radio />}
                            label={option}
                          />
                        ))}
                      </RadioGroup>
                    </FormControl>
                  ) : FormFieldTypes[Number(formField.fieldTypeId) - 1].type ==
                    "Select" ? (
                    <FormControl
                      style={{
                        marginBottom: "20px",
                        width: "100%",
                        maxWidth: "800px ",
                      }}
                    >
                      <FormLabel>{formField.lable}</FormLabel>
                      <Select
                        name={formField.lable}
                        value={formData[formField.lable]}
                        onChange={(e) => {
                          handlechange(e, index);
                        }}
                        required
                      >
                        {JSON.parse(formField.options).map((option) => (
                          <MenuItem key={cnt++} value={option}>
                            {option}
                          </MenuItem>
                        ))}
                      </Select>
                    </FormControl>
                  ) : (
                    <></>
                  )}
                </div>
              ))}
              <Button
                type="button"
                onClick={() => {
                  handleRemove(index);
                }}
              >
                remove
              </Button>
            </div>
          ))}
          <Button type="button" onClick={handleAddPerson}>
            Add Person
          </Button>

          <Button
            style={{
              marginBottom: "20px",
              width: "100%",
              maxWidth: "800px",
            }}
            type="submit"
            variant="contained"
            color="primary"
          >
            Register
          </Button>
        </form>
      ) : (
        <Transactions
          transactionData={{
            event: event,
            TotalPrice: TotalPrice,
            NoOfTickets: eventRegistrationFormFields.length,
            RegisteredData,
          }}
        />
      )}
    </>
  );
};

export default RegisterEvent;
