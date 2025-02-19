
import React, { useState, useEffect } from "react";
import { Link, useNavigate } from "react-router-dom"; // for navigation
import { Button, TextField, Grid } from "@mui/material";
import { useCreateCafeMutation, useUpdateCafeMutation } from '../../api/cafeApi';
import { useGetCafeByIdQuery } from '../../api/cafeApi';
import TextBox from "../Controls/TextBox";
import { MuiFileInput } from 'mui-file-input'


const CafeForm = ({ initialCafeData }) => {


  const [cafeId,setCafeId] = useState(initialCafeData?.id || null);
  const [name, setName] = useState(initialCafeData?.name || "");
  const [description, setDescription] = useState(initialCafeData?.description || "");
  const [logo, setLogo] = useState(null);
  const [location, setLocation] = useState(initialCafeData?.location || "");
  const [unsavedChanges, setUnsavedChanges] = useState(false);
  const navigate = useNavigate(); // Use useNavigate for navigation
  const [createCafe] = useCreateCafeMutation();
  const [updateCafe] = useUpdateCafeMutation();

  const { data: cafeData, isLoading, isError } = useGetCafeByIdQuery(cafeId);

  // Track form changes to warn on navigate
  useEffect(() => {
    const handleBeforeUnload = (event) => {
      if (unsavedChanges) {
        event.returnValue = true;
      }
    };



    window.addEventListener("beforeunload", handleBeforeUnload);

    return () => {
      window.removeEventListener("beforeunload", handleBeforeUnload);
    };
  }, [unsavedChanges]);

  const validateForm = () => {
    return (
      name.length >= 6 &&
      name.length <= 10 &&
      description.length <= 256 &&
      //(logo ? logo.size <= 2 * 1024 * 1024 : true) && // Max 2MB logo
      location
    );
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    if (validateForm()) {
      const cafeData = { name, description, logo, location };

      alert(JSON.stringify(cafeData, null, 2));

      setUnsavedChanges(false);
      if (cafeId) {
        await updateCafe({ id: cafeId, ...cafeData });
      } else {

        await createCafe(cafeData);
      }
      navigate("/"); // navigate back to café page
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
      //CANNOT FIND vairaible reader


      //   setLogo(file);
      //   setUnsavedChanges(true);
    } else {
      alert("File size should not exceed 2MB");
    }
  };



  return (

    <div style={{ maxWidth: 600, margin: "0 auto" }}>
      <Link to="/">Back to Cafes</Link>
      <h2>{cafeId ? 'Edit Cafe' : 'Add New Cafe'}</h2>
      <form onSubmit={handleSubmit}>
        <Grid container spacing={2}>
          {cafeId && <img src={cafeData?.logo} alt="logo" style={{ width: 100, height: 100, objectFit: "contain" }} />}
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
    //         <TextField
    //           label="Name"
    //           value={name}
    //           onChange={(e) => { setName(e.target.value); setUnsavedChanges(true); }}
    //           required
    //           fullWidth
    //           inputProps={{ maxLength: 10 }}
    //           helperText="Min 6 characters, max 10 characters"
    //         />
    //       </Grid>
    //       <Grid item xs={12}>
    //         <TextField
    //           label="Description"
    //           value={description}
    //           onChange={(e) => { setDescription(e.target.value); setUnsavedChanges(true); }}
    //           required
    //           fullWidth
    //           multiline
    //           inputProps={{ maxLength: 256 }}
    //           helperText="Max 256 characters"
    //         />
    //       </Grid>
    //       <Grid item xs={12}>
    //         <input

    //           type="file"
    //           onChange={handleLogoChange}
    //           accept="image/*"
    //           required
    //         />
    //       </Grid>
    //       <Grid item xs={12}>
    //         <TextField
    //           label="Location"
    //           value={location}
    //           onChange={(e) => { setLocation(e.target.value); setUnsavedChanges(true); }}
    //           required
    //           fullWidth
    //         />
    //       </Grid>
    //     </Grid>

    //     <div style={{ marginTop: 20, display: "flex", justifyContent: "space-between" }}>
    //       <Button onClick={handleCancel} variant="outlined">Cancel</Button>
    //       <Button type="submit" variant="contained" disabled={!validateForm()}>
    //         {cafeId ? "Update Cafe" : "Create Cafe"}
    //       </Button>
    //     </div>
    //   </form>
    // </div>
  // );
};

export default CafeForm;