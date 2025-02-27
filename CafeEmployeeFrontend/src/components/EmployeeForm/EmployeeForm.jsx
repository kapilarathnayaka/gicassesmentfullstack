import React, { useState, useEffect } from 'react';
import { useNavigate, Link } from 'react-router-dom';
import { Button, TextField, FormControl, FormLabel, RadioGroup, FormControlLabel, Radio, MenuItem, Select, InputLabel, Box, Grid } from '@mui/material';
import { useCreateEmployeeMutation, useUpdateEmployeeMutation } from '../../api/employeeApi';
import { useGetCafesQuery } from '../../api/cafeApi';
import TextBox from '../Controls/TextBox';

const EmployeeForm = ({ initialData }) => {
  console.log("renderring employee form");
  const [employeeId] = useState(initialData?.id || null);
  const [name, setName] = useState(initialData?.name || '');
  const [emailaddress, setEmail] = useState(initialData?.emailAddress || '');
  const [phonenumber, setPhone] = useState(initialData?.phoneNumber || '');
  const [gender, setGender] = useState(initialData?.gender || '');
  const [cafeId, setcafeId] = useState(initialData?.cafeId || '');
  const [isDirty, setIsDirty] = useState(false);
  const [genderError, setGenderError] = useState('');

  const navigate = useNavigate();
  const { data: cafes } = useGetCafesQuery();
  const [createEmployee] = useCreateEmployeeMutation();
  const [updateEmployee] = useUpdateEmployeeMutation();

  const validateForm = () => {
    return (
      name.length >= 6 &&
      name.length <= 10 &&
      phonenumber.length === 8 &&
      // cafeId &&
      gender
    );
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    if (!validateForm()) {
      return;
    }

    const employeeData = {
      name,
      emailaddress,
      phonenumber,
      gender,
      cafeId,
    };

    if (employeeId) {
      var response = await updateEmployee({ id: employeeId, ...employeeData });
      if (response.error) {
        alert(response.error.data)
      }
      if (response.data.success) {
        alert(response.data.message);
      }
    }
    else {
      var response = await createEmployee(employeeData);
      if (response.error) {
        alert('Error occurred while creating Employee.');
      }
      else {
        alert(response.data.message);
      }
    }
    navigate('/employee');
  };

  const handleCancel = () => {
    if (isDirty && !window.confirm('You have unsaved changes, do you want to discard them?')) {
      return;
    }
    navigate('/employee');
  };

  return (
    <Box
      display="flex"
      justifyContent="center"
      alignItems="center"
      height="100vh"
      width="100%"
      flexDirection="column"
    >
      <div style={{ maxWidth: 600, width: '100%' }}>
        <Link to="/employee">Back to Employees</Link>
        <h2>{employeeId ? 'Edit Employee' : 'Add New Employee'}</h2>
        <form onSubmit={handleSubmit}>
          <Grid container spacing={2}>
            <Grid item xs={12}>
              <TextBox label="Name" value={name} maxLength={10} minLength={6} required
                onChange={(e) => { setName(e); setUnsavedChanges(true); }}
              />
            </Grid>
            <Grid item xs={12}>
              <TextBox label="Email Address" value={emailaddress} type="email"
                onChange={(e) => { setEmail(e); setUnsavedChanges(true); }}
              />
            </Grid>
            <Grid item xs={12}>
              <TextBox label="Phone Number" value={phonenumber}
                maxLength={8} required
                onChange={(e) => { setPhone(e); setUnsavedChanges(true); }}
                type="phone" />
            </Grid>

            <Grid item xs={12}>
              <FormControl component="fieldset" error={!!genderError}>
                <FormLabel component="legend">Gender</FormLabel>
                {/* <RadioGroup value={gender} onChange={handleGenderChange}> */}
                <RadioGroup value={gender} onChange={(e) => { setGender(e.target.value); setIsDirty(true) }}>
                  <FormControlLabel value="Male" control={<Radio />} label="Male" />
                  <FormControlLabel value="Female" control={<Radio />} label="Female" />
                  <FormControlLabel value="Other" control={<Radio />} label="Other" />
                </RadioGroup>
              </FormControl>
              {genderError && <p style={{ color: 'red' }}>{genderError}</p>}
            </Grid>
            <Grid item xs={12}>
              <FormControl fullWidth>
                <InputLabel id="cafeLabelId">Assigned Café</InputLabel>
                {/* <Select labelId='cafeLabelId' value={cafeId} onChange={handleCafeChange} label="Assigned Café"> */}
                <Select labelId='cafeLabelId' value={cafeId} onChange={(e) => { setcafeId(e.target.value); setIsDirty(true) }} label="Assigned Café">
                  {cafes?.map((cafe) => (
                    <MenuItem key={cafe.id} value={cafe.id}>
                      {cafe.name}
                    </MenuItem>
                  ))}
                </Select>
              </FormControl>
            </Grid>
          </Grid>
          <div style={{ marginTop: '20px' }}>
          </div>
          <div style={{ marginTop: 20, display: "flex", justifyContent: "space-between" }}>
            <Button onClick={handleCancel} variant="outlined">Cancel</Button>
            <Button type="submit" variant="contained" disabled={!validateForm()}>
              {employeeId ? "Update Employee" : "Create Employee"}
            </Button>
          </div>
        </form>
      </div>
    </Box>
  );
};

export default EmployeeForm;
