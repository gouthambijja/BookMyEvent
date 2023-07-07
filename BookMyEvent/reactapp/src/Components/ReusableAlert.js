import React, { useState, useEffect } from 'react';
import { Snackbar, Alert } from '@mui/material';

const ReusableAlert = ({ message, severity, duration, onClose }) => {
    const [open, setOpen] = useState(true);

    useEffect(() => {
        const timer = setTimeout(() => {
            setOpen(false);
            onClose();
        }, duration);

        return () => clearTimeout(timer);
    }, [duration, onClose]);

    return (
        <Snackbar open={open} autoHideDuration={duration} onClose={() => setOpen(false)}>
            <Alert severity={severity} onClose={() => setOpen(false)}>{message}</Alert>
        </Snackbar>
    );
};

export default ReusableAlert;