
import React, { useState, useEffect } from "react";
import { Link, useNavigate } from "react-router-dom";
import { Button, Grid } from "@mui/material";
import { useCreateCafeMutation, useUpdateCafeMutation } from '../../api/cafeApi';
import TextBox from "../Controls/TextBox";
import Loader from "../Loader/Loader";

const CafeForm = ({ initialCafeData }) => {
const navigate = useNavigate();

const [cafeId] = useState(initialCafeData?.id || null);
const [name, setName] = useState(initialCafeData?.name || "");
const [description, setDescription] = useState(initialCafeData?.description || "");
const [logo, setLogo] = useState(initialCafeData?.logo || "");
const [location, setLocation] = useState(initialCafeData?.location || "");
const [unsavedChanges, setUnsavedChanges] = useState(false);


const [createCafe] = useCreateCafeMutation();
// const [updateCafe] = useUpdateCafeMutation();
const [updateCafe, { isLoading, error }] = useUpdateCafeMutation();



  // Track form changes to warn on navigate
  useEffect(() => {
    const handleBeforeUnload = (event) => {
      if (unsavedChanges) {
        event.returnValue = true;
      }
    };
    window.addEventListener("beforeunload", handleBeforeUnload);

    //cleanup function to avoid memory leaks
    return () => {
      window.removeEventListener("beforeunload", handleBeforeUnload);
    };
  }, [unsavedChanges]);

  const validateForm = () => {
    return (
      name.length >= 6 &&
      name.length <= 10 &&
      description.length <= 256 &&
      location
    );
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    if (validateForm()) {
      const cafeData = { name, description, logo, location };
      setUnsavedChanges(false);
      if (cafeId) {
        var response  = await updateCafe({ id: cafeId, ...cafeData });        
       

        if(response.data.success){
          alert(response.data.message);
        }
        else{
          alert('Cafe Not Found');
        }
      } else {
        var response = await createCafe(cafeData);
        alert(response.data.message)
      }
      navigate("/");
    } else {
      alert("Please fix the form validation errors.");
    }
  };

  const handleCancel = () => {
    if (unsavedChanges && !window.confirm("You have unsaved changes. Are you sure you want to cancel?")) {
      return;
    }
    navigate("/"); // navigate back to café page
  };

  const handleLogoChange = (e) => {
    const file = e.target.files[0];
    if (file && file.size <= 2 * 1024 * 1024) {
      const reader = new FileReader();
      reader.onload = (e) => {
        setLogo(e.target.result); // Set Base64 string
      };
      reader.readAsDataURL(file); // Convert file to Base64
    } else {
      alert("File size should not exceed 2MB");
    }
  };

  return (
    <div style={{ maxWidth: 600, margin: "0 auto" }}>
      <Link to="/">Back to Cafes</Link>
      {/* {isLoading && <Loader/>} */}
      <h2>{cafeId ? 'Edit Cafe' : 'Add New Cafe'}</h2>
      <form onSubmit={handleSubmit}>
        <Grid container spacing={2}>
          {cafeId && <img src={logo} alt="logo" style={{ width: 100, height: 100, objectFit: "contain" }} />}
          <Grid item xs={12}>
          <TextBox label="Name" minLength={6} maxLength={10} required value={name} 
          onChange={(e) => { setName(e); setUnsavedChanges(true); }}
          />
          </Grid>
          <Grid item xs={12}>
          <TextBox label="Description" maxLength={256} required value={description}
          onChange={(e) => { setDescription(e); setUnsavedChanges(true); }}
          />
          </Grid>
          <Grid item xs={12}>
          <TextBox label="Location" required value={location}
          onChange={(e) => { setLocation(e); setUnsavedChanges(true); }}
          />
          </Grid>
          <Grid item xs={12}>
          <input type="file" onChange={handleLogoChange} accept="image/*" required/>
          </Grid>
        </Grid>
        <div style={{ marginTop: 20, display: "flex", justifyContent: "space-between" }}>
          <Button onClick={handleCancel} variant="outlined">Cancel</Button>
          <Button type="submit" variant="contained" disabled={!validateForm()}>
            {cafeId ? "Update Cafe" : "Create Cafe"}
          </Button>
        </div>
      </form>
    </div>
  );
};

export default CafeForm;