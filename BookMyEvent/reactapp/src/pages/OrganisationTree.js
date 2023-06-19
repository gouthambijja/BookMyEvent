import React,{ useState } from 'react';
import { makeStyles } from '@material-ui/core/styles';
import List from '@material-ui/core/List';
import ListItem from '@material-ui/core/ListItem';
import ListItemText from '@material-ui/core/ListItemText';
import store from "../App/store";
import { RotateLeftOutlined } from '@material-ui/icons';
import Card from '@material-ui/core/Card';
import CardContent from '@material-ui/core/CardContent';
import Typography from '@material-ui/core/Typography';



const useStyles = makeStyles({
    root: {
        // width: '100%',
        // maxWidth: 360,
        display: 'flex',
        flexWrap: 'wrap',
        flexDirection: 'column',
        // justifyContent: 'space-between',
        alignItems: 'center',
    },
    section: {
        marginBottom: '1rem',
        display: 'flex',
        flexWrap: 'wrap',
        justifyContent: 'center',
    },
    sectionTitle: {
        fontWeight: 'bold',
        marginBottom: '0.5rem',
        fontSize: '1.5rem', // Adjust the font size as needed
        textAlign: 'center',
        color: '#333', // Set the desired color
      },
    card: {
        width: '250px',
        height: '300px',
        marginBottom: '0.5rem',
        marginTop:'1rem',
        padding:'15px',
        marginRight: '1rem',
        backgroundColor: '#FFF',
        borderRadius: '4px',
        boxShadow: '0px 0px 10px rgba(0, 0, 0, 0.5)',
        transition: 'transform 0.@s ease',
        '&:hover': {
          transform: 'scale(1.01)',
        },
      },
    cardContent: {
        display: 'flex',
        flexDirection: 'column',
        alignItems: 'center',
        padding: '1rem',
      },
      memberImageContainer: {
        width: '100px',
        height: '100px',
        marginBottom: '0.5rem',
        borderRadius: '50%',
        overflow: 'hidden',
      },
      memberImage: {
        width: '100%',
        height: '100%',
        objectFit: 'cover',
      },
      memberName: {
        fontWeight: 'bold',
        color: '#444',
        marginBottom: '0.5rem',
      },
      memberRole: {
        fontSize: '0.9rem',
        color: '#666',
        marginBottom: '0.5rem',
      },
      memberEmail: {
        fontSize: '0.9rem',
        color: '#666',
        marginBottom: '0.5rem',

      },
     
      filterButton: {
        // margin: '0 0.5rem',
        
        margin: '0.5rem 0.5rem',
        padding: '0.5rem 1rem',
        border: 'none',
        borderRadius: '4px',
        fontWeight: 'bold',
        cursor: 'pointer',
        
        backgroundColor: '#3f50b5',
        color: '#fff',
        transition: 'background-color 0.3s ease',
        '&:hover': {
          backgroundColor: '#555',
        },
      },
      activeButton: {
        backgroundColor: '#f44336',
      },
});
const OrganisationTree = () => {
    const orgMembers = store.getState().organisers.myOrganisationOrganisers;
    const [selectedRole, setSelectedRole] = useState('2');
    console.log(orgMembers);
    const handleRoleFilter = (roleId) => {
        setSelectedRole(roleId);
      };
    const classes = useStyles();
    const membersByRole = {};

// Group members by role
orgMembers.forEach((member) => {
  const { roleId } = member;
  
  if (!membersByRole[roleId]) {
    membersByRole[roleId] = [];
  }

  membersByRole[roleId].push(member);
});
    return (<>

        <div className={classes.buttonContainer}>
        <button
          className={`${classes.filterButton} ${selectedRole === '2' && classes.activeButton}`}
          onClick={() => handleRoleFilter('2')}
        >
          Owner
        </button>
        <button
          className={`${classes.filterButton} ${selectedRole === '3' && classes.activeButton}`}
          onClick={() => handleRoleFilter('3')}
        >
          Secondary Owner
        </button>
        <button
          className={`${classes.filterButton} ${selectedRole === '4' && classes.activeButton}`}
          onClick={() => handleRoleFilter('4')}
        >
          Peers
        </button>
      </div>
        <div className={classes.root}>
            
                <div >
                    {/* <h2 className={classes.sectionTitle}> {selectedRole == 2 ? "Owner" : selectedRole == 3 ? "Secondary Owners" : "Peers"}</h2> */}
                <div  className={classes.section}>
                    {membersByRole[selectedRole]!=null?<>
                    {membersByRole[selectedRole].map((member, index) => (
                        <Card key={index} className={classes.card}>

                            <CardContent className={classes.cardContent}>
                                <Typography variant="h6" component="h2" className={classes.memberImageContainer}>
                                    <img
                                        src={`data:image/jpeg;base64,${member.imgBody}`}
                                        alt={member.administratorName}
                    className={classes.memberImage}
                                    ></img>
                                </Typography>
                                <Typography variant="h6" component="h2"  className={classes.memberName}>
                                    {member.administratorName}
                                </Typography>
                                <Typography color="textSecondary" gutterBottom className={classes.memberRole}>
                                    Address: {member.administratorAddress}
                                </Typography>
                                <Typography color="textSecondary" className={classes.memberEmail}>
                                    Email: {member.email}
                                </Typography>
                                <Typography color="textSecondary" className={classes.memberEmail}>
                                    Phone: {member.phoneNumber}
                                </Typography>
                            </CardContent>
                        </Card>
                    ))}
                    </>:<>No Members to display</>}
                    </div>
                </div>
            
        </div>
    </>)
}
export default OrganisationTree;