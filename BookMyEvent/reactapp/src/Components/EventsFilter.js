import React, { useState } from 'react';
import { makeStyles } from '@material-ui/core/styles';
import {
    TextField,
    Checkbox,
    FormControlLabel,
    Button,
} from '@material-ui/core';
import OutlinedInput from '@mui/material/OutlinedInput';
import InputLabel from '@mui/material/InputLabel';
import MenuItem from '@mui/material/MenuItem';
import FormControl from '@mui/material/FormControl';
import ListItemText from '@mui/material/ListItemText';
import Select from '@mui/material/Select';
import Slider from '@mui/material/Slider';
import Box from '@mui/material/Box';
import { useSelector } from 'react-redux';
import store from '../App/store';
import { City } from 'country-state-city';
import Autocomplete from '@mui/material/Autocomplete';

const useStyles = makeStyles((theme) => ({
    root: {
        display: 'flex',
        flexDirection: 'column',
        gap: theme.spacing(2),
        justifyContent: 'center',
        margin: theme.spacing(2, 0),
    },
    button: {
        alignSelf: 'flex-end',
    },
}));


const EventsFilter = ({ onFilter }) => {
    const categories = useSelector((store) => store.category.categories);
    const classes = useStyles();
    const [startDate, setStartDate] = useState('');
    const [endDate, setEndDate] = useState('');
    const [priceRange, setPriceRange] = useState([0, 99999999]);
    const [startPrice, setStartPrice] = useState('');
    const [endPrice, setEndPrice] = useState('');
    const [location, setLocation] = useState('');
    const [isFree, setIsFree] = useState(false);
    const [selectedCategories, setSelectedCategories] = useState([]);
    //const allCities = City.getAllCities().map((city) => ({
    //    label: city.name,
    //    value: city.name,
    //}));

    const cities = City.getAllCities;
    //console.log(allCities[1]);
    //console.log(allCities[2]);
    const handleFilter = () => {
        const filter = {
            startDate,
            endDate,
            startPrice,
            endPrice,
            location,
            isFree,
            selectedCategories,
        };
        onFilter(filter);
    };

    const handlePriceChange = (event, newValue) => {
        setPriceRange(newValue);
        setStartPrice(newValue[0]);
        setEndPrice(newValue[1]);
    };

    const handleChange = (event) => {
        setSelectedCategories(event.target.value);
    };

    return (
        <div className={classes.root}>

            <FormControl sx={{ m: 1, width: 300 }}>
                <InputLabel id="demo-multiple-checkbox-label">Categories</InputLabel>
                <Select
                    labelId="demo-multiple-checkbox-label"
                    id="demo-multiple-checkbox"
                    multiple
                    value={selectedCategories}
                    onChange={handleChange}
                    input={<OutlinedInput label="Categories" />}
                    renderValue={(selected) => selected.map((categoryId) => {
                        const category = categories.find((c) => c.id === categoryId);
                        return category ? category.name : '';
                    }).join(', ')}
                >
                    {categories.map((category) => (
                        <MenuItem key={category.id} value={category.id}>
                            <Checkbox checked={selectedCategories.includes(category.id)} />
                            <ListItemText primary={category.name} />
                        </MenuItem>
                    ))}
                </Select>
            </FormControl>
            <TextField
                label="Start Date"
                type="date"
                value={startDate}
                onChange={(e) => setStartDate(e.target.value)}
                InputLabelProps={{
                    shrink: true,
                }}
            />
            <TextField
                label="End Date"
                type="date"
                value={endDate}
                onChange={(e) => setEndDate(e.target.value)}
                InputLabelProps={{
                    shrink: true,
                }}
            />
            <Box sx={{ width: 300 }}>
                <Slider
                    getAriaLabel={() => 'Price Range'}
                    value={priceRange}
                    onChange={handlePriceChange}
                    valueLabelDisplay="auto"
                    min={0}
                    max={99999}
                    sx={{ width: 300 }}
                />
            </Box>
            <FormControl sx={{ m: 1, width: 300 }}>
                <InputLabel id="demo-autocomplete-label">City</InputLabel>
                <Select
                    labelId="demo-autocomplete-label"
                    id="demo-autocomplete"
                    value={location}
                    onChange={handleChange}
                    input={<OutlinedInput label="City" />}
                    renderValue={(selected) => selected}
                    
                >
                    {cities.map((city) => (
                        <MenuItem key={city.id} value={city.name}>
                            <Checkbox checked={location === city.name} />
                            <ListItemText primary={city.name} />
                        </MenuItem>
                    ))}
                </Select>
            </FormControl>


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
            <Button
                variant="contained"
                color="primary"
                className={classes.button}
                onClick={handleFilter}
            >
                Filter
            </Button>
        </div>
    );
};

export default EventsFilter;

