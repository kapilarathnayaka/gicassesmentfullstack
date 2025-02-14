import React, { useState, useEffect } from 'react';
import { useHistory, useParams } from 'react-router-dom';
import { TextField, Button, Typography, RadioGroup, FormControlLabel, Radio, MenuItem, Select, InputLabel, FormControl } from '@material-ui/core';
import axios from 'axios';

const AddEditEmployeePage = () => {
  const [employee, setEmployee] = useState({
    name: '',
    emailAddress: '',
    phoneNumber: '',
    gender: '',
    cafeId: ''
  });
  const [cafes, setCafes] = useState([]);
  const [unsavedChanges, setUnsavedChanges] = useState(false);
  const history = useHistory();
  const { id } = useParams();

  useEffect(() => {
    fetchCafes();
    if (id) {
      fetchEmployee(id);
    }
  }, [id]);

  const fetchCafes = async () => {
    try {
      const response = await axios.get('/api/cafe');
      setCafes(response.data);
    } catch (error) {
      console.error('Error fetching cafes:', error);
    }
  };

  const fetchEmployee = async (id) => {
    try {
      const response = await axios.get(`/api/employee/${id}`);
      setEmployee(response.data);
    } catch (error) {
      console.error('Error fetching employee:', error);
    }
  };

  const handleChange = (event) => {
    const { name, value } = event.target;
    setEmployee((prevEmployee) => ({
      ...prevEmployee,
      [name]: value
    }));
    setUnsavedChanges(true);
  };

  const handleSubmit = async (event) => {
    event.preventDefault();
    try {
      if (id) {
        await axios.put(`/api/employee/${id}`, employee);
      } else {
        await axios.post('/api/employee', employee);
      }

      history.push('/employees');
    } catch (error) {
      console.error('Error saving employee:', error);
    }
  };

  const handleCancel = () => {
    if (unsavedChanges && !window.confirm('You have unsaved changes. Are you sure you want to leave?')) {
      return;
    }
    history.push('/employees');
  };

  return (
    <div>
      <Typography variant="h4">{id ? 'Edit Employee' : 'Add New Employee'}</Typography>
      <form onSubmit={handleSubmit}>
        <TextField
          label="Name"
          name="name"
          value={employee.name}
          onChange={handleChange}
          required
          inputProps={{ minLength: 6, maxLength: 10 }}
        />
        <TextField
          label="Email Address"
          name="emailAddress"
          value={employee.emailAddress}
          onChange={handleChange}
          required
          type="email"
        />
        <TextField
          label="Phone Number"
          name="phoneNumber"
          value={employee.phoneNumber}
          onChange={handleChange}
          required
          inputProps={{ pattern: '^[89]\\d{7}$' }}
        />
        <FormControl component="fieldset">
          <RadioGroup
            name="gender"
            value={employee.gender}
            onChange={handleChange}
            required
          >
            <FormControlLabel value="Male" control={<Radio />} label="Male" />
            <FormControlLabel value="Female" control={<Radio />} label="Female" />
          </RadioGroup>
        </FormControl>
        <FormControl>
          <InputLabel id="cafe-label">Assigned Café</InputLabel>
          <Select
            labelId="cafe-label"
            name="cafeId"
            value={employee.cafeId}
            onChange={handleChange}
            required
          >
            {cafes.map((cafe) => (
              <MenuItem key={cafe.id} value={cafe.id}>
                {cafe.name}
              </MenuItem>
            ))}
          </Select>
        </FormControl>
        <Button type="submit">Submit</Button>
        <Button onClick={handleCancel}>Cancel</Button>
      </form>
    </div>
  );
};

export default AddEditEmployeePage;
