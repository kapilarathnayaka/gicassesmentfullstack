import React, { useState, useEffect } from 'react';
import { useHistory, useParams } from 'react-router-dom';
import { TextField, Button, Typography } from '@material-ui/core';
import axios from 'axios';

const AddEditCafePage = () => {
  const [cafe, setCafe] = useState({
    name: '',
    description: '',
    logo: null,
    location: ''
  });
  const [unsavedChanges, setUnsavedChanges] = useState(false);
  const history = useHistory();
  const { id } = useParams();

  useEffect(() => {
    if (id) {
      fetchCafe(id);
    }
  }, [id]);

  const fetchCafe = async (id) => {
    try {
      const response = await axios.get(`/api/cafe/${id}`);
      setCafe(response.data);
    } catch (error) {
      console.error('Error fetching cafe:', error);
    }
  };

  const handleChange = (event) => {
    const { name, value } = event.target;
    setCafe((prevCafe) => ({
      ...prevCafe,
      [name]: value
    }));
    setUnsavedChanges(true);
  };

  const handleFileChange = (event) => {
    setCafe((prevCafe) => ({
      ...prevCafe,
      logo: event.target.files[0]
    }));
    setUnsavedChanges(true);
  };

  const handleSubmit = async (event) => {
    event.preventDefault();
    try {
      const formData = new FormData();
      formData.append('name', cafe.name);
      formData.append('description', cafe.description);
      formData.append('logo', cafe.logo);
      formData.append('location', cafe.location);

      if (id) {
        await axios.put(`/api/cafe/${id}`, formData);
      } else {
        await axios.post('/api/cafe', formData);
      }

      history.push('/cafes');
    } catch (error) {
      console.error('Error saving cafe:', error);
    }
  };

  const handleCancel = () => {
    if (unsavedChanges && !window.confirm('You have unsaved changes. Are you sure you want to leave?')) {
      return;
    }
    history.push('/cafes');
  };

  return (
    <div>
      <Typography variant="h4">{id ? 'Edit Cafe' : 'Add New Cafe'}</Typography>
      <form onSubmit={handleSubmit}>
        <TextField
          label="Name"
          name="name"
          value={cafe.name}
          onChange={handleChange}
          required
          inputProps={{ minLength: 6, maxLength: 10 }}
        />
        <TextField
          label="Description"
          name="description"
          value={cafe.description}
          onChange={handleChange}
          required
          inputProps={{ maxLength: 256 }}
        />
        <input
          type="file"
          name="logo"
          onChange={handleFileChange}
          accept="image/*"
        />
        <TextField
          label="Location"
          name="location"
          value={cafe.location}
          onChange={handleChange}
          required
        />
        <Button type="submit">Submit</Button>
        <Button onClick={handleCancel}>Cancel</Button>
      </form>
    </div>
  );
};

export default AddEditCafePage;
