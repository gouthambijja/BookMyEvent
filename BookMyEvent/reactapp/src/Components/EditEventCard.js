import { useEffect, useState } from "react";
import { Country, State, City } from "country-state-city";
import { useDispatch, useSelector } from "react-redux";
import { useNavigate, useParams } from "react-router-dom";
import eventsServices from "../Services/EventServices";
import { styled } from "@mui/system";
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
import store from "../App/store";
import { updateEvent } from "../Features/ReducerSlices/EventsSlice";
import { toast } from 'react-toastify';


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
const EditEventCard = () => {
    const { id } = useParams();
    const dispatch=useDispatch();
    const navigate= useNavigate();
    const profile=store.getState().profile.info;
    const events = useSelector((state) => state.events.organisationEvents);
    const categoryOptions = useSelector((store) => store.category.categories);
    const [event, setEvent] = useState(events.find((e) => e.eventId == id));
    const countryOptions = Country.getAllCountries();
    const [stateOptions, setStateOptions] = useState([]);
    const [cityOptions, setCityOptions] = useState([]);
    const [countryIsoCode, setCountryIsoCode] = useState("");
    const [EventCategory, SetEventCategory] = useState("");
    const [SelectedCountry, setSelectedCountry] = useState("");
    const [SelectedState, setSelectedState] = useState("");
    const [SelectedCity, setSelectedCity] = useState("");
    let count = 1;

    //console.log(event);
    const [eventData, setEventData] = useState({});
    //console.log(eventData);

    const handleChange = (e) => {
        let { name, value, type, checked } = e.target;
        if (e.target.name == "EventCategory") {
            SetEventCategory(value);
            let categoryId = categoryOptions.find(
                (e) => e.categoryName == value
            ).categoryId;
            name = "CategoryId";
            value = categoryId;
        }
        else if (e.target.name == "Country") {
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
        if (type == "checkbox") {
            value = checked;
            if (checked) {
                setEventData((prevstate) => ({
                    ...prevstate,
                    EventStartingPrice: 0,
                    EventEndingPrice: 0
                }));
            }
        }
        setEventData((prevstate) => ({
            ...prevstate,
            [name]: value,
        }));
    }
    const handleSubmit = async(e) => {
        e.preventDefault();
        //console.log(eventData);
       await dispatch(updateEvent(eventData));
       toast.success("Event Edited Successfully!")
        navigate(-1);
    }
    useEffect(() => {
        const temp = async () => {
            //console.log("inside temp")
            if (event == undefined) {
                const event = await eventsServices().getEventById(id);
                setEvent(event);
            }
        }
        //console.log("inside edit use effect")
        temp();
        
        if (event) {
            //console.log(event?.categoryId);
         

            let catName = categoryOptions.find((e) => e.categoryId == event?.categoryId).categoryName;
            //console.log(catName);
            SetEventCategory(catName);
            setSelectedCountry(event?.country);
            
            const isoCode = countryOptions.find((e) => e.name == event?.country).isoCode;
            setCountryIsoCode(isoCode);
            setStateOptions(State.getStatesOfCountry(isoCode));
            const stateIsoCode = State.getStatesOfCountry(isoCode).find((e) => e.name == event?.state).isoCode
            setSelectedState(event?.state);

            setCityOptions(
                City.getCitiesOfState(
                    isoCode,
                    stateIsoCode
                )
            );
            setSelectedCity(event?.city);
            setEventData((prevData) => ({
                ...prevData,
                EventId: event?.eventId,
                EventName: event?.eventName,
                StartDate: event?.startDate ? event.startDate.split("T")[0] : "",
                EndDate: event?.endDate ? event.endDate.split("T")[0] : "",
                Capacity: event?.capacity,
                Description: event?.description,
                Country: event?.country,
                State: event?.state,
                City: event?.city,
                Location: event?.location,
                MaxNoOfTicketsPerTransaction: event?.maxNoOfTicketsPerTransaction,
                EventStartingPrice: event?.eventStartingPrice,
                EventEndingPrice: event?.eventEndingPrice,
                IsFree: event.isFree,
                CategoryId:event?.categoryId,
                OrganisationId:event?.organisationId,
                AcceptedBy:event?.acceptedBy,
                CreatedBy: event?.createdBy,
                UpdatedBy:profile.administratorId,
                RegistrationStatusId:event?.registrationStatusId,
                IsActive:true,
                IsPublished:false,


            }))
        
    }
    }, [event]);
    return (<>
      

        <ColorfulForm onSubmit={handleSubmit}>

            <Typography variant="h5" align="center" color="primary">
               Edit Event Details
            </Typography>

            <TextField
                label="Event Name"
                name="EventName"
                value={eventData.EventName}
                onChange={handleChange}
                fullWidth
                required
            />

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
            <TextField
                label="Event Starting Price"
                name="EventStartingPrice"
                type="number"
                inputProps={{ min: 0 }}
                value={eventData.IsFree ? 0 : eventData.EventStartingPrice}
                onChange={handleChange}
                fullWidth
                required
            />
            <TextField
                label="Event Ending Price"
                name="EventEndingPrice"
                type="number"
                inputProps={{
                    min: eventData.IsFree ? 0 : eventData.EventStartingPrice,
                }}
                value={eventData.IsFree ? 0 : eventData.EventEndingPrice}
                onChange={handleChange}
                fullWidth
                required
            />
            <FormControlLabel
                control={
                    <Checkbox
                        name="IsFree"
                        checked={eventData.IsFree===true}
                        onChange={handleChange}
                        // defaultChecked={eventData.IsFree?true:false}
                    />
                }
                label="Is Free"
            />
            <ColorfulButton type="submit" variant="contained" fullWidth>
                Submit
            </ColorfulButton>
        </ColorfulForm>
    </>)
}
export default EditEventCard;