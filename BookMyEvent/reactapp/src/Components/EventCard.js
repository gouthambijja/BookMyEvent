import * as React from 'react';
import {useEffect} from 'react';
import Card from '@mui/material/Card';
import CardContent from '@mui/material/CardContent';
import CardMedia from '@mui/material/CardMedia';
import Typography from '@mui/material/Typography';
import { Button, CardActionArea, CardActions } from '@mui/material';
import { useSelector } from 'react-redux';

const EventCard = ({event}) =>  {
    const categories = useSelector(store => store.category);
    useEffect(()=>{
    },[])
  return (
    <Card className="card" sx={{width:'300px',boxShadow:"0px 0px 9px #d0d0d0"} }>
      <CardActionArea>
        <CardMedia
          component="img"
          sx={{width:'100%',aspectRatio:1/1}}
          image={`data:image/jpeg;base64,${event.profileImgBody}`}
          alt="green iguana"
        />
        <CardContent >
          <Typography variant="h4" component="div">
          {event.eventName}
        </Typography>
          <Typography variant="h6" component="div">
          {categories.categories[Number(event.categoryId)]?.categoryName}
        </Typography>
        <Typography variant="subtitle1" color="text.secondary">
          {event.startDate?.split("T")[0]} to {event.endDate.split("T")[0]}
        </Typography>
        <Typography variant="subtitle1" color="text.secondary">
          {event.location}
        </Typography>
        <Typography variant="subtitle1" color="text.secondary">
         {event.city},{event.state}, {event.country}
        </Typography>
        <Typography variant="body1" color="text.primary">
          {event.eventStartingPrice == 0? "Free":`Tickets starts from ${event.eventStartingPrice}` }
        </Typography>
        </CardContent>
      </CardActionArea>
      <CardActions sx={{display:'flex',justifyContent:'right'}}>
        <Button size="small" color="primary">
          Register
        </Button>
      </CardActions>
    </Card>
  );
}
export default EventCard;
