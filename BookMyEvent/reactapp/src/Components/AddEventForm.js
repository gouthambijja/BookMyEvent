// import React, { useState } from "react";
// import {
//   TextField,
//   Button,
//   FormControlLabel,
//   Checkbox,
//   Typography,
//   FormControl,
//   InputLabel,
//   Select,
//   MenuItem,
//   Box,
// } from "@mui/material";
// import { styled } from "@mui/system";
// import { useSelector } from "react-redux";
// import { name } from "ejs";

// const ColorfulForm = styled("form")({
//   display: "flex",
//   flexDirection: "column",
//   gap: "1rem",
//   maxWidth: "800px",
//   margin: "0 auto",
//   padding: "1rem",
//   backgroundColor: "#fff",
//   borderRadius: "8px",
// });

// const ColorfulButton = styled(Button)({
//   backgroundColor: "#3f50b5",
//   color: "#fff",
//   "&:hover": {
//     backgroundColor: "#d81b60",
//   },
// });

// const EventForm = () => {
//   const categoryOptions = useSelector((store) => store.category.categories);
//   const FormFields = useSelector((store) => store.formFields.formFields);
//   console.log(FormFields);
//   const [eventData, setEventData] = useState({
//     EventId: "",
//     EventName: "",
//     StartDate: "",
//     EndDate: "",
//     Capacity: "",
//     Description: "",
//     Location: "",
//     IsOffline: false,
//     MaxNoOfTicketsPerTransaction: "",
//     EventStartingPrice: "",
//     EventEndingPrice: "",
//     IsFree: false,
//     IsActive: false,
//     OrganisationId: "",
//   });
//   const [inputFields, setInputFields] = useState([{ value: "",fieldType:"",fieldLabel:"",validations:"",options:"" ,required:false}]);

//   const handleInputChange = (index, event) => {
//     const {name,value,type,checked} = event.target;
//     const values = [...inputFields];
//     values[index][name] = type === "checkbox" ? checked : value;
//     setInputFields(values);
//   };

//   const handleAddFields = () => {
//     setInputFields([...inputFields, { value: "" }]);
//   };

//   const handleRemoveFields = (index) => {
//     const values = [...inputFields];
//     values.splice(index, 1);
//     setInputFields(values);
//   };

//   const handleSubmit = (e) => {
//     e.preventDefault();
//     // Perform form submission or data processing here
//     console.log(eventData);
//   };

//   const handleChange = (e) => {
//     let { name, value, type, checked } = e.target;
//     console.log(name, value, type, checked);
//     if (e.target.name == "EventCategory") {
//       SetEventCategory(value);
//       let categoryId = categoryOptions.find(
//         (e) => e.categoryName == value
//       ).categoryId;
//       name = "categoryId";
//       value = categoryId;
//     }
//     console.log(value);

//     setEventData((prevState) => ({
//       ...prevState,
//       [name]: type === "checkbox" ? checked : value,
//     }));
//     console.log(eventData);
//   };
//   const [EventCategory, SetEventCategory] = useState("");
//   return (
//     <ColorfulForm onSubmit={handleSubmit}>
//       <Typography variant="h5" align="center" color="primary">
//         Event Details
//       </Typography>
//       <TextField
//         label="Event Name"
//         name="EventName"
//         value={eventData.EventName}
//         onChange={handleChange}
//         fullWidth
//         required
//       />
//       <TextField
//         label="Start Date"
//         name="StartDate"
//         type="date"
//         value={eventData.StartDate}
//         onChange={handleChange}
//         fullWidth
//         required
//         InputLabelProps={{ shrink: true }}
//       />
//       <TextField
//         label="End Date"
//         name="EndDate"
//         type="date"
//         value={eventData.EndDate}
//         onChange={handleChange}
//         fullWidth
//         required
//         InputLabelProps={{ shrink: true }}
//       />
//       <FormControl fullWidth>
//         <InputLabel>Select Category</InputLabel>
//         <Select
//           required
//           name="EventCategory"
//           value={EventCategory}
//           onChange={handleInputChange}
//         >
//           {categoryOptions.map((option, index) => (
//             <MenuItem
//               key={Math.floor(Math.random() * 100)}
//               value={option.categoryName}
//             >
//               {option.categoryName}
//             </MenuItem>
//           ))}
//         </Select>
//       </FormControl>
//       <TextField
//         label="Capacity"
//         name="Capacity"
//         type="number"
//         value={eventData.Capacity}
//         onChange={handleChange}
//         fullWidth
//         required
//       />
//       <TextField
//         label="Description"
//         name="Description"
//         value={eventData.Description}
//         onChange={handleChange}
//         fullWidth
//         multiline
//         required
//         rows={4}
//       />
//       <TextField
//         label="Location"
//         name="Location"
//         value={eventData.Location}
//         onChange={handleChange}
//         fullWidth
//         required
//       />
//       <FormControlLabel
//         control={
//           <Checkbox
//             name="IsOffline"
//             checked={eventData.IsOffline}
//             onChange={handleChange}
//           />
//         }
//         label="Is Offline"
//       />
//       <TextField
//         label="Max No. of Tickets per Transaction"
//         name="MaxNoOfTicketsPerTransaction"
//         type="number"
//         value={eventData.MaxNoOfTicketsPerTransaction}
//         onChange={handleChange}
//         fullWidth
//         required
//       />
//       <TextField
//         label="Event Starting Price"
//         name="EventStartingPrice"
//         type="number"
//         value={eventData.EventStartingPrice}
//         onChange={handleChange}
//         fullWidth
//         required
//       />
//       <TextField
//         label="Event Ending Price"
//         name="EventEndingPrice"
//         type="number"
//         value={eventData.EventEndingPrice}
//         onChange={handleChange}
//         fullWidth
//         required
//       />
//       <FormControlLabel
//         control={
//           <Checkbox
//             name="IsFree"
//             checked={eventData.IsFree}
//             onChange={handleChange}
//           />
//         }
//         label="Is Free"
//       />

//       <Box
//         sx={{
//           border: "1px solid #d0d0d0",
//           borderRadius: "4px",
//           padding: "20px",
//         }}
//       >
//         <Box sx={{ fontSize: "1.2rem", marginBottom: "10px" }}>
//           {" "}
//           Event Registration Form Fields
//         </Box>
//         {inputFields.map((inputField, index) => (
//           <div key={index}>
//             <Select
//               required
//               name="FieldType"
//               value={EventCategory}
//               onChange={handleChange}
//             >
//               {categoryOptions.map((option, index) => (
//                 <MenuItem
//                   key={Math.floor(Math.random() * 100)}
//                   value={option.categoryName}
//                 >
//                   {option.categoryName}
//                 </MenuItem>
//               ))}
//             </Select>
//             <TextField
//               label="Field Label"
//               value={inputField.value}
//               onChange={(event) => handleInputChange(index, event)}
//             />
//             <Button
//               variant="outlined"
//               onClick={() => handleRemoveFields(index)}
//             >
//               Remove
//             </Button>
//           </div>
//         ))}
//         <ColorfulButton type="button" variant="contained" fullWidth>
//           Add Form Field
//         </ColorfulButton>
//       </Box>
//       <ColorfulButton type="submit" variant="contained" fullWidth>
//         Submit
//       </ColorfulButton>
//     </ColorfulForm>
//   );
// };

// export default EventForm;
