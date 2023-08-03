import React, { useState } from 'react';
import PropTypes from 'prop-types';
import {
    Button,
    Dialog,
    DialogActions,
    DialogContent,
    DialogContentText,
    DialogTitle,
    TextField,
} from '@material-ui/core';

const ConfirmationDialog = ({ open, title, content, onConfirm, onCancel, showReason = false }) => {
    const [reason, setReason] = useState('');

    const handleConfirm = () => {
        onConfirm(showReason ? reason : null);
        setReason('');
    };

    const handleCancel = () => {
        onCancel();
        setReason('');
    };

    const handleReasonChange = (event) => {
        setReason(event.target.value);
    };
    return (
        <Dialog open={open} onClose={handleCancel}>
            <DialogTitle>{title}</DialogTitle>
            <DialogContent>
                <DialogContentText>{content}</DialogContentText>
                {showReason && (
                    <TextField
                        label="Reason"
                        value={reason}
                        onChange={handleReasonChange}
                        fullWidth
                    />
                )}
            </DialogContent>
            <DialogActions>
                <Button onClick={handleCancel} color="primary">
                    Cancel
                </Button>
                <Button onClick={handleConfirm} color="primary">
                    Confirm
                </Button>
            </DialogActions>
        </Dialog>
    );
};

ConfirmationDialog.propTypes = {
    open: PropTypes.bool.isRequired,
    title: PropTypes.string.isRequired,
    content: PropTypes.string.isRequired,
    onConfirm: PropTypes.func.isRequired,
    onCancel: PropTypes.func.isRequired,
    showReason: PropTypes.bool,
};

export default ConfirmationDialog;