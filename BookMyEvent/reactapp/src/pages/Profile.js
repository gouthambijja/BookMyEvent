import { Call, LocationCity, Mail, MyLocation } from "@material-ui/icons";
import React from "react";
import { useSelector } from "react-redux";

const ProfilePage = () => {
  // Sample data
  const profile = useSelector((store) => store.profile.info);
  const auth= useSelector(store => store.auth);
    console.log(profile);
  return (
    <div style={{minHeight:'calc(100vh - 116px)'}} className="ProfilePage">
      <div
        style={{ display: "flex", flexWrap:"wrap" }}
      >
        <div
        className="profileImage"
          style={{
            padding:"20px 0px",
            display: "flex",
            justifyContent: "center",
            alignItems: "center",
            width:'100%'
          }}
        >
          <img
            src={profile.imgBody !==""?`data:image/jpeg;base64,${profile.imgBody}`:profile.imageName}
            style={{borderRadius:'4px',width:'300px',maxWidth:'90%', maxHeight: "300px" }}
          ></img>
        </div>
        <div style={{display:'flex',flexBasis:'100%'}}>
        <div
        className="profileInfo"
          style={{ display: "flex",flexBasis:'100%', flexDirection: "column", padding: "20px" }}
        >
          <div style={{ textAlign:'center',fontVariant:'smallcaps',fontSize: "2rem", fontWeight: "bold",padding:'15px'}}>
            {auth.role=="User"?profile.name:profile.administratorName}
          </div>
          <div style={{ marginTop: "10px", display: "flex" ,flexWrap:'wrap',justifyContent:'space-around',padding:'15px'}}>
            <div style={{textAlign:'center'}}>
              <LocationCity style={{fontSize:'72px'}} />
              <p >{auth.role=="User"?profile.userAddress:profile.administratorAddress}</p>
            </div>
            <div style={{textAlign:'center'}}>
                <Mail style={{fontSize:'72px'}}/>
                <p>{profile.email}</p>
            </div>
            <div style={{textAlign:'center'}}>
                <Call style={{fontSize:'72px'}}/>
               <p>{profile.phoneNumber}</p> 
            </div>
          </div>
        </div>
        </div>
        
      </div>
    </div>
  );
};

export default ProfilePage;
