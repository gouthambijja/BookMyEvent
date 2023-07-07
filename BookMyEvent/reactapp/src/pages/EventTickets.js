import React, { useEffect, useState } from "react";
import { Typography, Card, CardContent } from "@material-ui/core";
import { useParams } from "react-router-dom";
import TicketServices from "../Services/TicketServices";
import {FontDownload} from "@material-ui/icons";
import { useSelector } from "react-redux";
import { Box, Button } from "@mui/material";
import Ticket from "../Components/Ticket";
import EventServices from "../Services/EventServices";
import OrganiserFormServices from "../Services/OrganiserFormServices";
import { DocumentScanner, Download, FileDownload, Upload, UploadFile } from "@mui/icons-material";
import exportFromJson from 'export-from-json'
import { make_cols } from "../Utils/MakeColumns";
import {read,utils} from 'xlsx'

const EventTickets = () => {
    const { eventId } = useParams();
    const [tickets, setTickets] = useState([]);
    useEffect(() => {
        const loadTickets = async () => {
          
            const _tickets = await TicketServices().getEventTickets(eventId);
            //console.log(_tickets);
            setTickets(_tickets);
        }
        loadTickets();

    }, []);
    const [event, setEvent] = useState({});
    useEffect(() => {
      const loadevent = async () => {
        const Event = await EventServices().getEventById(eventId);
        //console.log(Event);
        setEvent(Event);
      };
      loadevent();
    }, []);
    let cnt = 0;
    const handleDownloadTemplate = async () =>{
      const formFields = await OrganiserFormServices().getFieldTypesByFormId(
        event.formId
      );
      let templateData = [];
      formFields.forEach(field => {
        templateData.push({[field.lable]:``});
      })
      const exportType =  exportFromJson.types.xls
      const fileName = `${event.eventName} Template`
      const data = templateData;
      exportFromJson({ data,fileName,exportType })

      console.log(templateData);
      console.log(formFields);
    }
    const [file,setFile] = useState();
    const handleFileChange = (e) =>{
      const files = e.target.files;
    if (files && files[0]) setFile(files[0] );
    }

    const handleFile = () => {
      /* Boilerplate to set up FileReader */
      const reader = new FileReader();
      const rABS = !!reader.readAsBinaryString;
   
      reader.onload = (e) => {
        /* Parse data */
        const bstr = e.target.result;
        const wb = read(bstr, { type: rABS ? 'binary' : 'array', bookVBA : true });
        /* Get first worksheet */
        const wsname = wb.SheetNames[0];
        const ws = wb.Sheets[wsname];
        /* Convert array of arrays */
        const data = utils.sheet_to_json(ws);
        /* Update state */
        // this.setState(, () => {
        //   console.log(JSON.stringify(this.state.data, null, 2));
        // });
        console.log( data );
      };
   
      
      if (rABS) {
        reader.readAsBinaryString(file);
      } else {
        reader.readAsArrayBuffer(file);
      };
    }

  return (
    <>
    <div style={{width:'100%',height:'34px',background:'#fff',display:'flex',justifyContent:'space-between',position:'sticky',top:'64px',zIndex:'20'}}>
      <Button onClick={handleDownloadTemplate} title="download template" >
        <FileDownload/>
      </Button>
      <form ><input type="file" style={{border:'1px solid #d0d0d0'}} accept=".xlsx,.xls" onChange={handleFileChange} ></input><Button sx={{marginLeft:'2px'}} title="upload bulk registrations" onClick={handleFile} >
        <UploadFile/>
      </Button></form>
      
    </div>
    <div
      id="ticketComponent"
      style={{
        padding: "30px",
        display: "flex",
        justifyContent: "center",
        flexWrap: "wrap",
        gap: "10px",
      }}
    >
      {tickets.length === 0 ? (
        <Typography variant="body1">No tickets available.</Typography>
      ) : (
        <>
          {tickets.map((ticket, index) => (
            <Ticket key={cnt++} ticket={{ ticket: ticket, index: index, event: event }} />
          ))} 
        </>
      )}
    </div>
    </>
  );
};

export default EventTickets;
