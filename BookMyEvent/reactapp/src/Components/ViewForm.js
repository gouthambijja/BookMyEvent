import React, { useEffect, useState } from "react";
import OrganiserFormServices from "../Services/OrganiserFormServices";
import { useDispatch, useSelector } from "react-redux";
import {
  FormControl,
  FormControlLabel,
  FormLabel,
  MenuItem,
  Radio,
  RadioGroup,
  Select,
  TextField,
} from "@mui/material";
import store from "../App/store";
import { getFormFieldsThunk } from "../Features/ReducerSlices/FormFieldsSlice";

const ViewForm = (formId) => {
  formId = formId.formId;
  const [formFields, setFormFields] = useState();
  const FormFieldTypes = useSelector((store) => store.formFields.formFields);
  const dispatch = useDispatch();
  useEffect(() => {
    const getFormFieldsUsingFormId = async () => {
      if (store.getState().formFields.formFields.length == 0) {
        await dispatch(getFormFieldsThunk()).unwrap();
      }
      const formFieldsResponse =
        await OrganiserFormServices().getFieldTypesByFormId(formId);
      console.log(formFieldsResponse);
      setFormFields(formFieldsResponse);
    };
    getFormFieldsUsingFormId();
  }, []);
  let cnt = 0;
  return (
    <div>
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
        {formFields?.map((formField) => (
          <div key={cnt++}>
            {console.log(formField.fieldTypeId)}
            {console.log(FormFieldTypes)}
            {FormFieldTypes[Number(formField.fieldTypeId) - 1]?.type ==
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
                required
              />
            ) : FormFieldTypes[Number(formField.fieldTypeId) - 1]?.type ==
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
                inputProps={{
                  min: Number(JSON.parse(formField.validations).min),
                  max: Number(JSON.parse(formField.validations).max),
                }}
                required
              />
            ) : FormFieldTypes[Number(formField.fieldTypeId) - 1]?.type ==
              "File" ? (
              <TextField
                style={{
                  marginBottom: "20px",
                  width: "100%",
                  maxWidth: "800px",
                }}
                type="file"
                name={formField.lable}
                label={formField.lable}
                required
              />
            ) : FormFieldTypes[Number(formField.fieldTypeId) - 1]?.type ==
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
                required
              />
            ) : FormFieldTypes[Number(formField.fieldTypeId) - 1]?.type ==
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
                required
              />
            ) : FormFieldTypes[Number(formField.fieldTypeId) - 1]?.type ==
              "Radio" ? (
              <FormControl
                style={{
                  marginBottom: "20px",
                  width: "100%",
                  maxWidth: "800px ",
                }}
              >
                <FormLabel>{formField.lable}</FormLabel>
                <RadioGroup name={formField.lable} required>
                  {JSON.parse(formField.options).map((option) => (
                    <div key={cnt++}>
                      {" "}
                      <FormControlLabel
                        value={option}
                        control={<Radio />}
                        label={option}
                      />
                    </div>
                  ))}
                </RadioGroup>
              </FormControl>
            ) : FormFieldTypes[Number(formField.fieldTypeId) - 1]?.type ==
              "Select" ? (
              <FormControl
                style={{
                  marginBottom: "20px",
                  width: "100%",
                  maxWidth: "800px ",
                }}
              >
                <FormLabel>{formField.lable}</FormLabel>
                <Select name={formField.lable} required>
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
      </div>
    </div>
  );
};

export default ViewForm;
