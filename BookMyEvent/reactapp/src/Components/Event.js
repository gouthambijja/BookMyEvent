import React from 'react';
import Card from '@mui/material/Card';
import CardActions from '@mui/material/CardActions';
import CardContent from '@mui/material/CardContent';
import Typography from '@mui/material/Typography';
import { CardMedia } from '@mui/material';
import './registerbutton.css'

export default function Event() {
    const handleRegister = () => {

    }
    return (
        <CardActions sx={{ cursor: 'pointer' }}>
            <Card sx={{ maxWidth: 250 ,height:435 }}>
                <CardMedia
                    component="img"
                    alt="green iguana"
                    height="200"
                    image='https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQYTb7anPWOF9_u89HL1Ekv9xwSv7SfdNw-Zg&usqp=CAU'
                />
                <CardContent>
                    <Typography gutterBottom variant="h5" component="div">
                        King Of Tollywood
                    </Typography>
                    <Typography variant="body2" color="text.secondary" component="div" sx={{height:100}}>
                        Uppalapati Venkata Suryanarayana Prabhas Raju (born 23 October 1979), known mononymously as Prabhas is an Indian actor who predominantly works in Telugu ...
                    </Typography>
                </CardContent>
                <CardActions>
                    <button class="cta" onClick={handleRegister}>
                        <span>Register</span>
                        <svg viewBox="0 0 13 10" height="10px" width="10px">
                            <path d="M1,5 L11,5"></path>
                            <polyline points="8 1 12 5 8 9"></polyline>
                        </svg>
                    </button>
                </CardActions>
            </Card>
        </CardActions>
    );
}