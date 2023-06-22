import React, { useState } from "react";
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
};

const EventsFilter = ({ onFilter }) => {
  const categories = useSelector((store) => store.category.categories);
  const [startDate, setStartDate] = useState();
  const [endDate, setEndDate] = useState();
  const [priceRange, setPriceRange] = useState([0, 99999999]);
  const [startPrice, setStartPrice] = useState();
  const [endPrice, setEndPrice] = useState();
  const [location, setLocation] = useState();
  const [isFree, setIsFree] = useState(false);
  const [categoryIds, setcategoryIds] = useState();
  const [Visibility, SetVisibility] = useState(false);
  const handleFilter = () => {
    const filter = {
      startDate: startDate ? startDate : new Date(1),
      endDate: endDate ? startDate : new Date(4000000000001),
      startPrice: startPrice ? startPrice : 0,
      endPrice: endPrice ? endPrice : 99999999,
      location: location ? location : "",
      isFree: isFree ? isFree : false,
      categoryIds: categoryIds ? categoryIds : [],
    };
    if (Visibility) {
      onFilter(filter);
    }
    SetVisibility((prev) => !prev);
  };

  const handlePriceChange = (event, newValue) => {
    setPriceRange(newValue);
    setStartPrice(newValue[0]);
    setEndPrice(newValue[1]);
  };

  const handleChange = (event) => {
    setcategoryIds(event.target.value);
  };

  return (
    <div>
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
            {/* <FormControl sx={{ m: 1, width: '100%' }}>
        <InputLabel id="demo-multiple-checkbox-label">Categories</InputLabel>
        <Select
          labelId="demo-multiple-checkbox-label"
          id="demo-multiple-checkbox"
          multiple
          value={categoryIds}
          onChange={handleChange}
          input={<OutlinedInput label="Categories" />}
          renderValue={(selected) =>
            selected
              .map((categoryId) => {
                const category = categories.find((c) => c.id === categoryId);
                return category ? category.name : "";
              })
              .join(", ")
          }
        >
          {categories.map((category) => (
            <MenuItem key={category.id} value={category.id}>
              <Checkbox checked={categoryIds.includes(category.id)} />
              <ListItemText primary={category.name} />
            </MenuItem>
          ))}
        </Select>
      </FormControl> */}
            <TextField
              label="Start Date"
              fullWidth
              type="date"
              value={startDate}
              onChange={(e) => setStartDate(e.target.value)}
              InputLabelProps={{
                shrink: true,
              }}
            />
            <TextField
              label="End Date"
              fullWidth
              type="date"
              value={endDate}
              onChange={(e) => setEndDate(e.target.value)}
              InputLabelProps={{
                shrink: true,
              }}
            />
            <Box sx={{ width: 300 }}>
              Price Range
              <Slider
                getAriaLabel={() => "Price Range"}
                value={priceRange}
                onChange={handlePriceChange}
                valueLabelDisplay="auto"
                min={0}
                max={10000}
                sx={{ width: 300 }}
              />
            </Box>
            <TextField
              label="Location"
              fullWidth
              type="text"
              value={endDate}
              onChange={(e) => setLocation(e.target.value)}
              InputLabelProps={{
                shrink: true,
              }}
            />

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
        <Button variant="contained" color="primary" onClick={handleFilter}>
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
