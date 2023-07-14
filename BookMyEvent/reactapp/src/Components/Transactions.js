import React from "react";
import {
    Card,
    CardContent,
    CardMedia,
    Typography,
    Button,
    Box,
} from "@mui/material";
import { useNavigate } from "react-router-dom";
import { useDispatch, useSelector } from "react-redux";
import TransationServices from "../Services/TransationServices";
import { UpdateAmountSpent, UpdateNoOfTransactions } from "../Features/ReducerSlices/HomeEventsSlice";
import { toast } from "react-toastify";
const Transactions = ({ transactionData }) => {
    const auth = useSelector(store => store.auth)
    const navigate = useNavigate();
    const dispatch = useDispatch();
    const eventImage = transactionData.event.profileImgBody;
    const numberOfTickets = transactionData.NoOfTickets;
    const totalPrice = transactionData.TotalPrice;
    const eventName = transactionData.event.eventName;
    const RegisteredData = transactionData.RegisteredData;
    const handlePay = async () => {
        const id = (auth.role == "User")?"UserId":"AdministratorId";
        const transactionInfo = {
            [id]: auth.id,
            EventId: transactionData.event.eventId,
            Amount: transactionData.TotalPrice,
            NoOfTickets: transactionData.NoOfTickets,
            IsSuccessful: true
        };
        const userInputFormData = [];
        for (let i = 0; i < RegisteredData.length; i++) {
            userInputFormData.push(RegisteredData[i].userInputFormBL);
        }
        const formData = { transaction: transactionInfo, ListOfUserInputForm: userInputFormData };
        try{
            await TransationServices().addTransaction(formData);
            dispatch(UpdateNoOfTransactions());
            dispatch(UpdateAmountSpent(totalPrice))
            toast.success("transaction successful!")
            console.log(auth.role);
            if(auth.role == "User")
            navigate(`/tickets/${transactionData.event.eventId}`);
            else{
            navigate(`/organiser`);}
        }
        catch{
            toast.error('payment failed!')
        }
    }
    return (
        <Box sx={{ width: '100%', height: "calc( 100vh - 64px )", display: "flex", justifyContent: "center", alignItems: "center" }}>
            <Box sx={{ width: '100%', maxWidth: "800px" }}>
                <Card>
                    <CardMedia
                        component="img"
                        height="500px"
                        image={`data:image/jpeg;base64,${eventImage}`}
                        alt={eventName}
                    />
                    <CardContent>
                        <Typography variant="h5" component="div">
                            {eventName}
                        </Typography>
                        <Typography variant="body1" color="text.secondary">
                            Number of Tickets: {numberOfTickets}
                        </Typography>
                        <Typography variant="body1" color="text.secondary">
                            Total Price: {totalPrice}
                        </Typography>

                        <Box sx={{ display: 'flex', justifyContent: 'end', }}>
                            <Button variant="contained" color="primary" onClick={handlePay}>
                                Pay {totalPrice} /-
                            </Button>
                            <Button variant="outlined" color="secondary" sx={{ marginLeft: '10px' }} onClick={() => { navigate('/') }}>
                                Cancel
                            </Button></Box>
                    </CardContent>
                </Card>
            </Box>
        </Box>
    );
};

export default Transactions;
