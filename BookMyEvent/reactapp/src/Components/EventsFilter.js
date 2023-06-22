import React, { useState } from 'react';
import { Slider, FormControlLabel, Checkbox, FormGroup, Autocomplete, TextField, Button } from '@mui/material';
import { City } from 'country-state-city';
import store from '../App/store';

const EventsFilter = ({ onFilter }) => {
    const categories = store.getState().category.categories;
    const [startDate, setStartDate] = useState(null);
    const [endDate, setEndDate] = useState(null);
    const [startPrice, setStartPrice] = useState(0);
    const [endPrice, setEndPrice] = useState(100);
    const [location, setLocation] = useState('');
    const [isFree, setIsFree] = useState(false);
    const [selectedCategories, setSelectedCategories] = useState([]);

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

    const handleCategoryChange = (event) => {
        setSelectedCategories(event.target.value);
    };

    const handleCityChange = (event, value) => {
        setLocation(value);
    };

    return (
        <div>
            <FormGroup>
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
                <div>
                    <div>Price Range</div>
                    <Slider
                        value={[startPrice, endPrice]}
                        onChange={(e, newValue) => {
                            setStartPrice(newValue[0]);
                            setEndPrice(newValue[1]);
                        }}
                        valueLabelDisplay="auto"
                        min={0}
                        max={100}
                    />
                </div>
                <Autocomplete
                    options={City.getAllCities()}
                    getOptionLabel={(option) => option.name}
                    renderInput={(params) => <TextField {...params} label="City" />}
                    onChange={handleCityChange}
                    value={location}
                />
                <FormControlLabel
                    control={<Checkbox checked={isFree} onChange={(e) => setIsFree(e.target.checked)} />}
                    label="Is Free"
                />
                <FormGroup>
                    <div>Categories</div>
                    {categories.map((category) => (
                        <FormControlLabel
                            key={category.id}
                            control={
                                <Checkbox
                                    checked={selectedCategories.includes(category.id)}
                                    onChange={handleCategoryChange}
                                    value={category.id}
                                />
                            }
                            label={category.categoryName}
                        />
                    ))}
                </FormGroup>
                <Button variant="contained" onClick={handleFilter}>
                    Filter
                </Button>
            </FormGroup>
        </div>
    );
};

export default EventsFilter;