import { Call, LocationCity, Mail, MyLocation } from "@material-ui/icons";
import React from "react";
import { useSelector } from "react-redux";

const ProfilePage = () => {
  // Sample data
  const profile = useSelector((store) => store.profile.info);
  const auth= useSelector(store => store.auth);
    console.log(profile);
  return (
    <div>
      <div
        style={{ margin: "20px", boxShadow:'0px 0px 10px #3f50b5',padding:'15px', display: "flex",flexWrap:'wrap !important' }}
      >
        <div
        className="profileImage"
          style={{
            display: "flex",
            justifyContent: "center",
            alignItems: "center",
            flexBasis:'30%'
          }}
        >
          <img
            src={`data:image/jpeg;base64,${profile.imgBody}`}
            style={{ width: "300px", height: "300px" }}
          ></img>
        </div>
        <div
        className="profileInfo"
          style={{ display: "flex", flexDirection: "column", padding: "20px" ,flexBasis:'70%'}}
        >
          <div style={{ minWidth:'350px',fontSize: "2rem", fontWeight: "bold",boxShadow:'0px 0px 10px 	#D0D0D0',padding:'15px' }}>
            {auth.role=="User"?profile.name:profile.administratorName}
          </div>
          <div style={{ marginTop: "10px", display: "flex" ,flexWrap:'wrap',justifyContent:'space-between',boxShadow:'0px 0px 10px 	#D0D0D0',padding:'15px'}}>
            <div >
              <LocationCity />
              <span >{auth.role=="User"?profile.userAddress:profile.administratorAddress}</span>
            </div>
            <div>
                <Mail/>
                {profile.email}
            </div>
            <div>
                <Call/>
                {profile.phoneNumber}
            </div>
          </div>
        </div>
      </div>
      <div
        style={{
          display: "flex",
          flexDirection: "column",
          justifyContent: "center",
        }}
      >
        <p>{profile.administratorName}</p>
        <span>
          <i>{profile.email}</i>
        </span>
      </div>
    </div>
  );
};

export default ProfilePage;
