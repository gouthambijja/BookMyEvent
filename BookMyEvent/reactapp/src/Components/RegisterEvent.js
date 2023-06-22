import React, { useState } from 'react';
import {
  TextField,
  Select,
  MenuItem,
  FormControl,
  FormLabel,
  RadioGroup,
  FormControlLabel,
  Radio,
  Button
} from '@mui/material';

const RegisterEvent = () => {
  const [formData, setFormData] = useState({
    name: '',
    email: '',
    age: '',
    gender: '',
    ticketType: '',
    tier: ''
  });

  const handleChange = (event) => {
    const { name, value } = event.target;
    setFormData((prevData) => ({
      ...prevData,
      [name]: value
    }));
  };

  const handleSubmit = (event) => {
    event.preventDefault();
    console.log(formData); // Perform form submission or further processing here
  };

  const formContainerStyle = {
    display: 'flex',
    flexDirection: 'column',
    alignItems: 'center',
    padding: '20px',
    backgroundColor: '#fff',
    borderRadius: '8px',
    boxShadow: '0 2px 4px rgba(0, 0, 0, 0.2)',
    transition: 'transform 0.3s ease',
    '&:hover': {
      transform: 'scale(1.02)'
    }
  };

  const formFieldStyle = {
    marginBottom: '20px',
    width: '300px'
  };

  const submitButtonStyle = {
    marginTop: '20px',
    backgroundColor: '#4caf50',
    color: '#fff',
    fontWeight: 'bold',
    borderRadius: '4px',
    padding: '12px 24px',
    cursor: 'pointer',
    border: 'none',
    boxShadow: '0 2px 4px rgba(0, 0, 0, 0.2)',
    transition: 'background-color 0.3s ease',
    textTransform: 'uppercase',
    letterSpacing: '1px',
    fontSize: '14px',
    '&:hover': {
      backgroundColor: '#45a049'
    }
  };

  return (
    <form style={formContainerStyle} onSubmit={handleSubmit}>
      <TextField
        style={formFieldStyle}
        type="text"
        name="name"
        label="Name"
        value={formData.name}
        onChange={handleChange}
        required
      />

      <TextField
        style={formFieldStyle}
        type="email"
        name="email"
        label="Email"
        value={formData.email}
        onChange={handleChange}
        required
      />

      <TextField
        style={formFieldStyle}
        type="number"
        name="age"
        label="Age"
        value={formData.age}
        onChange={handleChange}
        inputProps={{
          min: 18,
          max: 100
        }}
        required
      />

      <FormControl style={formFieldStyle}>
        <FormLabel>Gender</FormLabel>
        <RadioGroup name="gender" value={formData.gender} onChange={handleChange} required>
          <FormControlLabel value="male" control={<Radio />} label="Male" />
          <FormControlLabel value="female" control={<Radio />} label="Female" />
          <FormControlLabel value="others" control={<Radio />} label="Others" />
        </RadioGroup>
      </FormControl>

      <FormControl style={formFieldStyle}>
        <FormLabel>Ticket Type</FormLabel>
        <Select name="ticketType" value={formData.ticketType} onChange={handleChange} required>
          <MenuItem value="1000">1000</MenuItem>
          <MenuItem value="500">500</MenuItem>
          <MenuItem value="750">750</MenuItem>
        </Select>
      </FormControl>

      <FormControl style={formFieldStyle}>
        <FormLabel>Select Tier</FormLabel>
        <Select name="tier" value={formData.tier} onChange={handleChange} required>
          <MenuItem value="Tier-1">Tier-1</MenuItem>
          <MenuItem value="Tier-2">Tier-2</MenuItem>
          <MenuItem value="Tier-3">Tier-3</MenuItem>
          <MenuItem value="Tier-4">Tier-4</MenuItem>
        </Select>
      </FormControl>

      <Button style={submitButtonStyle} type="submit" variant="contained" color="primary">
        Submit
      </Button>
    </form>
  );
};

export default RegisterEvent;
