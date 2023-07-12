import React, { useEffect, useRef, useState } from "react";
import { Typography, Card, CardContent } from "@material-ui/core";
import { useParams } from "react-router-dom";
import TicketServices from "../Services/TicketServices";
import { FontDownload } from "@material-ui/icons";
import { useSelector } from "react-redux";
import { Box, Button } from "@mui/material";
import Ticket from "../Components/Ticket";
import EventServices from "../Services/EventServices";
import OrganiserFormServices from "../Services/OrganiserFormServices";
import {
  DocumentScanner,
  Download,
  FileDownload,
  Upload,
  UploadFile,
} from "@mui/icons-material";
import exportFromJson from "export-from-json";
import { make_cols } from "../Utils/MakeColumns";
import { read, utils } from "xlsx";
import store from "../App/store";
import UserInputFormServices from "../Services/UserInputFormServices"
import { toast } from "react-toastify";
import Transactions from "../Components/Transactions";

const EventTickets = () => {
  const { eventId } = useParams();
  const [tickets, setTickets] = useState([]);
  useEffect(() => {
    const loadTickets = async () => {
      const _tickets = await TicketServices().getEventTickets(eventId);
      //console.log(_tickets);
      setTickets(_tickets);
    };
    loadTickets();
  }, []);
  const formFields = useSelector((store) => store.formFields.formFields);
  const formFieldDateId = formFields.find((e) => e.type == "Date").fieldTypeId;
  const [event, setEvent] = useState({});
  useEffect(() => {
    const loadevent = async () => {
      const Event = await EventServices().getEventById(eventId);
      setEvent(Event);
    };
    loadevent();
  }, []);
  let cnt = 0;
  const handleDownloadTemplate = async () => {
    const formFields = await OrganiserFormServices().getFieldTypesByFormId(
      event.formId
    );
    let templateData = [];
    console.log(formFields);
    formFields.forEach((field) => {
      templateData.push({ [field.lable]: `` });
    });
    const exportType = exportFromJson.types.xls;
    const fileName = `${event.eventName} Template`;
    const data = templateData;
    exportFromJson({ data, fileName, exportType });
    console.log(templateData);
    console.log(formFields);
  };
  const [file, setFile] = useState();
  const [RegisteredData, setRegisteredData] = useState();
  const [toggleTicketsTransaction,setToggleTicketsTransaction]= useState(true);
  const [TotalPrice, setTotalPrice] = useState(0);

  const handleFileChange = (e) => {
    const files = e.target.files;
    if (files && files[0]) setFile(files[0]);
  };
  let formResult = [];
  const [noOfTickets,setNoOfTickets] = useState(0);

  const handleFile = () => {
    /* Boilerplate to set up FileReader */
    const reader = new FileReader();
    const rABS = !!reader.readAsBinaryString;

    reader.onload = async (e) => {
      /* Parse data */
      const bstr = e.target.result;
      const wb = read(bstr, { type: rABS ? "binary" : "array", bookVBA: true });
      /* Get first worksheet */
      const wsname = wb.SheetNames[0];
      const ws = wb.Sheets[wsname];
      /* Convert array of arrays */
      const data = utils.sheet_to_json(ws);

      /* Update state */
      // this.setState(, () => {
      //   console.log(JSON.stringify(this.state.data, null, 2));
      // });
      const formFields = await OrganiserFormServices().getFieldTypesByFormId(
        event.formId
      );
      let fieldRegistrationId = {}

      formFields.forEach((e) => {
        fieldRegistrationId =  { ...fieldRegistrationId, [e.lable]: e.registrationFormFieldId };
      });

      let dateColumns = [];
      formFields.forEach((e) => {
        if (e.fieldTypeId == formFieldDateId) {
          dateColumns.push(e.lable);
        }
      });
      for (let i = 0; i < data.length; i++) {
        dateColumns.forEach((e) => {
          const baseDate = new Date(1900, 0, 1);
          data[i][e] = new Date(
            baseDate.getTime() + (Number(data[i][e]) - 1) * 24 * 60 * 60 * 1000
          ).toISOString();
        });
      }
      console.log(data);
      
      
      
      //-------------------------------------------------------------------------------------------------------------------

    formResult = [];
    data.forEach((e, index) => {
      let formFieldResponse = [];
      for (let i in e) {
        const type = formFields.find((u) => {
          return u.lable == i;
        }).fieldTypeId;
        console.log(e,i,fieldRegistrationId);
        if (type == 1 || type == 3 || type == 6 || type == 5 ) {
          formFieldResponse.push({
            label: i,
            stringResponse: `${e[i]}`,
            registrationFormFieldId: fieldRegistrationId[i],
          });
          if (i == "Ticket Prices") {
            setTotalPrice((prev) => prev + Number(e[i]));
            //console.log(TotalPrice);
          }
        } else if (type == 2) {
          formFieldResponse.push({
            label: i,
            numberResponse: e[i],
            registrationFormFieldId: fieldRegistrationId[i],
          });
        } else if (type == 4) {
          const date = new Date(e[i]);
          formFieldResponse.push({
            label: i,
            dateResponse: date,
            registrationFormFieldId: fieldRegistrationId[i],
          });
        }
      }
      formResult.push(formFieldResponse);
    });
    let formResultPost = [];
    for (let i = 0; i < formResult.length; i++) {
      formResultPost.push({
        ["userInputFormBL"]: {
          ["administratorId"]: store.getState().auth.id,
          ["eventId"]: event.eventId,
        },
        ["userInputFormFields"]: formResult[i],
      });
    }
    console.log(formResultPost);
    setNoOfTickets(formResultPost.length);
    try{
      setRegisteredData(
        await UserInputFormServices().submitUserInputForm(formResultPost)
        );
      setToggleTicketsTransaction(false);
    }catch{
      toast.error('failed to upload registrations!');     
    }


      //-------------------------------------------------------------------------------------------------------------------
    };

    if (rABS) {
      reader.readAsBinaryString(file);
    } else {
      reader.readAsArrayBuffer(file);
    }
  };

  return (
    <>
    {toggleTicketsTransaction?<>
      <div
        style={{
          width: "100%",
          height: "34px",
          background: "#fff",
          display: "flex",
          justifyContent: "space-between",
          position: "sticky",
          top: "64px",
          zIndex: "20",
        }}
      >
        <Button onClick={handleDownloadTemplate} title="download template">
          <FileDownload />
        </Button>
        <form>
          <input
            type="file"
            style={{ border: "1px solid #d0d0d0" }}
            accept=".xlsx,.xls"
            onChange={handleFileChange}
          ></input>
          <Button
            sx={{ marginLeft: "2px" }}
            title="upload bulk registrations"
            onClick={handleFile}
          >
            <UploadFile />
          </Button>
        </form>
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
              <Ticket
                key={cnt++}
                ticket={{ ticket: ticket, index: index, event: event }}
              />
            ))}
          </>
        )}
      </div>
    </>: <Transactions
          transactionData={{
            event: event,
            TotalPrice: TotalPrice,
            NoOfTickets: noOfTickets,
            RegisteredData,
          }}
        />}
    </>
    
  );
};

export default EventTickets;
