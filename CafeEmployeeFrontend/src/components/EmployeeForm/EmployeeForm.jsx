import React, { useState, useEffect } from 'react';
import { useNavigate, Link } from 'react-router-dom';
import { Button, TextField, FormControl, FormLabel, RadioGroup, FormControlLabel, Radio, MenuItem, Select, InputLabel, Box, Grid } from '@mui/material';
import { useCreateEmployeeMutation, useUpdateEmployeeMutation } from '../../api/employeeApi';
import { useGetCafesQuery } from '../../api/cafeApi';
import TextBox from '../Controls/TextBox';



const EmployeeForm = ({ initialData }) => {

  const [employeeId, setEmployeeId] = useState(initialData?.id || null);
  const [name, setName] = useState(initialData?.name || '');
  const [emailaddress, setEmail] = useState(initialData?.emailaddress || '');
  // const [emailaddress, setEmail] = useState('');
  const [phonenumber, setPhone] = useState('');
  const [gender, setGender] = useState('male');
  const [cafeId, setcafeId] = useState('');
  const [isDirty, setIsDirty] = useState(false);
  const [nameError, setNameError] = useState('');
  const [emailError, setEmailError] = useState('');
  const [phoneError, setPhoneError] = useState('');
  const [genderError, setGenderError] = useState('');

  const navigate = useNavigate();
  const { data: cafes } = useGetCafesQuery();
  const [createEmployee] = useCreateEmployeeMutation();
  const [updateEmployee] = useUpdateEmployeeMutation();


  const handleNameChange = (e) => {
    setName(e.target.value);
    setIsDirty(true);
  };

  const handleEmailChange = (e) => {
    setEmail(e.target.value);
    setIsDirty(true);
  };

  const handlePhoneChange = (e) => {
    setPhone(e.target.value);
    setIsDirty(true);
  };

  const handleGenderChange = (e) => {
    setGender(e.target.value);
    setIsDirty(true);
  };

  const handleCafeChange = (e) => {
    setcafeId(e.target.value);
    setIsDirty(true);
  };

  const validateForm = () => {
    return (
      name.length >= 6 &&
      name.length <= 10 &&
      phonenumber.length === 8 &&
      cafeId
      //description.length <= 256 &&
      //(logo ? logo.size <= 2 * 1024 * 1024 : true) && // Max 2MB logo
  //    location
    );
  };


  const validateFormx = () => {
    let valid = true;

    // Name validation
    if (name.length < 6 || name.length > 10) {
      setNameError('Name must be between 6 and 10 characters.');
      valid = false;
    } else {
      setNameError('');
    }

    // Email validation
    const emailPattern = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;
    if (!emailPattern.test(emailaddress)) {
      setEmailError('Invalid emailaddress address.');
      valid = false;
    } else {
      setEmailError('');
    }

    // Phone validation (SG phonenumber number)
    const phonePattern = /^(8|9)\d{7}$/;
    if (!phonePattern.test(phonenumber)) {
      setPhoneError('Phone number must start with 8 or 9 and have 8 digits.');
      valid = false;
    } else {
      setPhoneError('');
    }

    // Gender validation
    if (!gender) {
      setGenderError('Please select a gender.');
      valid = false;
    } else {
      setGenderError('');
    }

    return valid;
  };

  const handleSubmit = async () => {
    if (!validateForm()) {
      return;
    }

    // const employeeData = {
    //   name,
    //   emailaddress,
    //   phonenumber,
    //   gender,
    //   cafeId,
    // };

    if (employeeId) {
      // Update existing employee
      await updateEmployee({ id: employeeId, ...employeeData });
    } else {
      // Create new employee
      await createEmployee(employeeData);
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
        <form>
          <Grid container spacing={2}>
            <Grid item xs={12}>
<TextBox label="Name" value={name} error={!!nameError} helperText={nameError} maxLength={10} minLength={6} required
onChange={(e) => { setName(e); setUnsavedChanges(true); }}
/>
            </Grid>
            <Grid item xs={12}>
<TextBox label="Email Address" value={emailaddress} type="email" error={!!emailError} helperText={emailError} 
onChange={(e) => { setEmail(e); setUnsavedChanges(true); }}
/>
            </Grid>
            <Grid item xs={12}>
<TextBox label="Phone Number" value={phonenumber} 
maxLength={8} required
onChange={(e) => { setPhone(e); setUnsavedChanges(true); }} 
type="phone" error={!!phoneError}  />
            </Grid>
            
            <Grid item xs={12}>
            <FormControl component="fieldset" error={!!genderError}>
            <FormLabel component="legend">Gender</FormLabel>
            <RadioGroup value={gender} onChange={handleGenderChange}>
              <FormControlLabel value="male" control={<Radio />} label="Male" />
              <FormControlLabel value="female" control={<Radio />} label="Female" />
              <FormControlLabel value="other" control={<Radio />} label="Other" />
            </RadioGroup>
          </FormControl>
          {genderError && <p style={{ color: 'red' }}>{genderError}</p>}
            </Grid>
            <Grid item xs={12}>
            <FormControl fullWidth>
            <InputLabel id="cafeLabelId">Assigned Café</InputLabel>
            <Select labelId='cafeLabelId' value={cafeId} onChange={handleCafeChange} label="Assigned Café">
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
                    <Button type="submit" variant="contained" onClick={handleSubmit} disabled={!validateForm()}>
                      {employeeId ? "Update Employee" : "Create Employee"}
                    </Button>
                  </div>
        </form>
      </div>
    </Box>
  );
};

//           <Textbox
//             label="Name"
//             value={name}
//             onChange={handleNameChange}
//             error={!!nameError}
//             helperText={nameError}
//           />
//           <Textbox
//             label="Email Address"
//             value={emailaddress}
//             onChange={handleEmailChange}
//             type="emailaddress"
//             error={!!emailError}
//             helperText={emailError}
//           />
//           <Textbox
//             label="Phone Number"
//             value={phonenumber}
//             onChange={handlePhoneChange}
//             type="tel"
//             error={!!phoneError}
//             helperText={phoneError}
//           />
//           <FormControl component="fieldset" error={!!genderError}>
//             <FormLabel component="legend">Gender</FormLabel>
//             <RadioGroup value={gender} onChange={handleGenderChange}>
//               <FormControlLabel value="male" control={<Radio />} label="Male" />
//               <FormControlLabel value="female" control={<Radio />} label="Female" />
//               <FormControlLabel value="other" control={<Radio />} label="Other" />
//             </RadioGroup>
//           </FormControl>
//           {genderError && <p style={{ color: 'red' }}>{genderError}</p>}
//           <FormControl fullWidth>
//             <InputLabel>Assigned Café</InputLabel>
//             <Select value={cafeId} onChange={handleCafeChange}>
//               {cafes?.map((cafe) => (
//                 <MenuItem key={cafe.id} value={cafe.id}>
//                   {cafe.name}
//                 </MenuItem>
//               ))}
//             </Select>
//           </FormControl>
//           <div style={{ marginTop: '20px' }}>
//             <Button variant="contained" color="primary" onClick={handleSubmit}>
//               {employeeId ? 'Update Employee' : 'Add Employee'}
//             </Button>
//             <Button variant="outlined" color="secondary" onClick={handleCancel} style={{ marginLeft: '10px' }}>
//               Cancel
//             </Button>
//           </div>
//         </form>
//       </div>
//     </Box>
//   );
// };

export default EmployeeForm;
