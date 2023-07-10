import React, { useState, useEffect } from "react";
import { makeStyles } from "@material-ui/core/styles";
import {
  TextField,
  Checkbox,
  FormControlLabel,
  Button,
} from "@material-ui/core";
import OutlinedInput from "@mui/material/OutlinedInput";
import InputLabel from "@mui/material/InputLabel";
import MenuItem from "@mui/material/MenuItem";
import FormControl from "@mui/material/FormControl";
import ListItemText from "@mui/material/ListItemText";
import Select from "@mui/material/Select";
import Slider from "@mui/material/Slider";
import Box from "@mui/material/Box";
import { useSelector } from "react-redux";
import store from "../App/store";
import { City } from "country-state-city";
import Autocomplete from "@mui/material/Autocomplete";

const filtersStyle = {
  position: "fixed",
  top: "64px",
  zIndex: "30",
  background: "rgba(255, 255, 255, 0.9)",
  padding: "30px",
  width: "100%",
  backdropFilter: "blur(6px)",
  borderBottom:"1px solid #d0d0d0",
};

const EventsFilter = ({ onFilter }) => {
  const categories = useSelector((store) => store.category.categories);
  const [location, setLocation] = useState('');
  const [isFree, setIsFree] = useState(false);
  const [categoryIds, setcategoryIds] = useState([]);
  const [Visibility, SetVisibility] = useState(false);
  const [name, setName] = useState("");
  const handleFilter = () => {
    const filter = {
      name:name?name:"",
      location: location ? location : "",
      isFree: isFree ? isFree : false,
      categoryIds: categoryIds ? categoryIds : [],
    };
    if (Visibility) {
      onFilter(filter);
    }
    SetVisibility((prev) => !prev);
  };
  useEffect(() => {
    //console.log(categoryIds);
  }, []);
  const handleCategoryChange = (id) => {
    id=id;
    if (categoryIds.includes(id)) {
      const cIds = [...categoryIds];
      cIds.splice(cIds.indexOf(id),1);
      setcategoryIds(cIds);
    } else {
      const cIds = [...categoryIds];
      cIds.push(id);
      setcategoryIds(cIds);
    }
  };
  return (
    <div >
      {Visibility ? (
        <div style={filtersStyle}>
          <div
            style={{
              maxWidth: "800px",
              position: "relative",
              left: "50%",
              transform: "translateX(-50%)",
            }}
          >
            <InputLabel sx={{fontSize:'12px'}}>Categories</InputLabel>
            <div style={{padding:'20px'}}>
              {categories.map((category) => (
                <FormControlLabel
                  key={category.categoryId}
                  control={
                    <Checkbox
                      checked={categoryIds.includes(category.categoryId)}
                      onChange={() => {handleCategoryChange(category?.categoryId)}}
                    />
                  }
                  label={category.categoryName}
                />
              ))}
            </div>
            <TextField
              label="Location"
              fullWidth
              variant="outlined"
              type="text"
              value={location}
              onChange={(e) => setLocation(e.target.value)}
              InputLabelProps={{
                shrink: true,
              }}
            />
            <Box sx={{marginTop:"10px"}}>
            <TextField
              label="EventName"
              fullWidth
              variant="outlined"
              type="text"
              value={name}
              onChange={(e) => setName(e.target.value)}
              InputLabelProps={{
                shrink: true,
              }}
              
            /></Box>

            <FormControlLabel
              control={
                <Checkbox
                  checked={isFree}
                  onChange={(e) => setIsFree(e.target.checked)}
                  color="primary"
                />
              }
              label="Is Free"
            />
          </div>
        </div>
      ) : (
        <></>
      )}
      <div
        style={{
          padding: "0px 10px",
          position: "fixed",
          right: "10px",
          top: "70px",
          zIndex: "30   ",
        }}
      >
        <Button variant="contained" color="primary" onClick={handleFilter} >
          {!Visibility ? "Filter" : "ApplyFilter"}
        </Button>
        {Visibility ? (
          <Button
            variant="contained"
            color="secondary"
            onClick={() => {
              SetVisibility((prev) => !prev);
            }}
          >
            Cancel
          </Button>
        ) : (
          <></>
        )}
      </div>
    </div>
  );
};

export default EventsFilter;
