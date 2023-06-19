import { useEffect, useState } from "react";
import store from "../App/store";
import { Table, TableBody, TableCell, TableContainer, TableHead, TableRow, Button,Paper } from '@mui/material';
import organiserServices from "../Services/OrganiserServices";
import OrganisationService from "../Services/OrganisationService";



const PeerRequest=()=>{
    const profile = store.getState().profile.info;
    const [requests,setRequests]=useState([]);
    
      const handleAccept = async(peer) => {
        peer.acceptedBy=profile.administratorId;
        peer.updatedOn=(new Date()).toLocaleString();
        await organiserServices.acceptOrganiser(peer);

        // Handle accept logic for the peer request with the given ID
        console.log(`Accepted peer request with ID: `);
      };
      const handleReject = (id) => {
        // Handle reject logic for the peer request with the given ID
        console.log(`Rejected peer request with ID: ${id}`);
      };
    useEffect(()=>{
        const temp=async()=>{
          const peers=await organiserServices.getRequestedPeers(profile.organisationId);
          setRequests(peers) ; 
        }
        temp();
    },[])

    return(<>
    <div>Peer requests</div>
    <TableContainer component={Paper} sx={{ boxShadow: '0 4px 10px rgba(0, 0, 0, 0.1)' }}>
      <Table sx={{ minWidth: 650 }}>
        <TableHead>
          <TableRow>
            <TableCell sx={{ fontWeight: 'bold' }}>Image</TableCell>
            <TableCell sx={{ fontWeight: 'bold' }}>Name</TableCell>
            <TableCell sx={{ fontWeight: 'bold' }}>Address</TableCell>
            <TableCell sx={{ fontWeight: 'bold' }}>Action</TableCell>
          </TableRow>
        </TableHead>
        <TableBody>
          {requests.map((peer) => (
            <TableRow key={peer.administratorId} sx={{
                '&:hover': { backgroundColor: '#f5f5f5' },
                '&:last-child td': { borderBottom: 'none' },
              }}>
              <TableCell> <img
            src={`data:image/jpeg;base64,${peer.imgBody}`}
            style={{ width: "100px", height: "100px" }}
          ></img></TableCell>
              <TableCell>{peer.administratorName}</TableCell>
              <TableCell>{peer.administratorAddress}</TableCell>
              <TableCell>
              <Button
                  variant="outlined"
                  onClick={() => handleAccept(peer)}
                  sx={{ marginRight: '10px', color: 'green' }}
                >
                  Accept
                </Button>
                <Button
                  variant="outlined"
                  onClick={() => handleReject(peer.administratorId)}
                  sx={{ color: 'red' }}
                >
                  Reject
                </Button>
              </TableCell>
            </TableRow>
          ))}
        </TableBody>
      </Table>
    </TableContainer>
    </>)

}
export default PeerRequest;