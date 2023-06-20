import * as React from 'react';
import {useEffect} from 'react';
import Card from '@mui/material/Card';
import CardContent from '@mui/material/CardContent';
import CardMedia from '@mui/material/CardMedia';
import Typography from '@mui/material/Typography';
import { Button, CardActionArea, CardActions } from '@mui/material';

const EventCard = ({event}) =>  {
    useEffect(()=>{

    })
  return (
    <Card sx={{width:'300px',boxShadow:"0px 0px 9px #d0d0d0"} }>
      <CardActionArea>
        <CardMedia
          component="img"
          sx={{width:'100%',aspectRatio:1/1}}
          image="https://images.pexels.com/photos/15193545/pexels-photo-15193545/free-photo-of-ballons-in-cappadocia.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
          alt="green iguana"
        />
        <CardContent>
          <Typography variant="h5" component="div">
          Shairngan
        </Typography>
        <Typography variant="subtitle1" color="text.secondary">
          Date: [DATE] | Time: [TIME]
        </Typography>
        <Typography variant="subtitle1" color="text.secondary">
          Location: [LOCATION]
        </Typography>
        <Typography variant="body1" color="text.primary">
          [EVENT DESCRIPTION TEXT]
        </Typography>
        </CardContent>
      </CardActionArea>
      <CardActions sx={{display:'flex',justifyContent:'right'}}>
        <Button size="small" color="primary">
          Share
        </Button>
      </CardActions>
    </Card>
  );
}
export default EventCard;
