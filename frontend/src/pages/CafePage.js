import React, { useState, useEffect } from 'react';
import { useHistory } from 'react-router-dom';
import { AgGridReact } from 'ag-grid-react';
import 'ag-grid-community/styles/ag-grid.css';
import 'ag-grid-community/styles/ag-theme-alpine.css';
import { Button, TextField } from '@material-ui/core';
import axios from 'axios';

const CafePage = () => {
  const [cafes, setCafes] = useState([]);
  const [location, setLocation] = useState('');
  const history = useHistory();

  useEffect(() => {
    fetchCafes();
  }, [location]);

  const fetchCafes = async () => {
    try {
      const response = await axios.get(`/api/cafe?location=${location}`);
      setCafes(response.data);
    } catch (error) {
      console.error('Error fetching cafes:', error);
    }
  };

  const handleLocationChange = (event) => {
    setLocation(event.target.value);
  };

  const handleAddCafe = () => {
    history.push('/add-edit-cafe');
  };

  const handleEditCafe = (id) => {
    history.push(`/add-edit-cafe/${id}`);
  };

  const handleDeleteCafe = async (id) => {
    if (window.confirm('Are you sure you want to delete this cafe?')) {
      try {
        await axios.delete(`/api/cafe/${id}`);
        fetchCafes();
      } catch (error) {
        console.error('Error deleting cafe:', error);
      }
    }
  };

  const handleViewEmployees = (cafeId) => {
    history.push(`/employees?cafe=${cafeId}`);
  };

  const columns = [
    { headerName: 'Logo', field: 'logo', cellRendererFramework: (params) => <img src={params.value} alt="Logo" style={{ width: '50px', height: '50px' }} /> },
    { headerName: 'Name', field: 'name' },
    { headerName: 'Description', field: 'description' },
    { headerName: 'Employees', field: 'employees', cellRendererFramework: (params) => <Button onClick={() => handleViewEmployees(params.data.id)}>{params.value}</Button> },
    { headerName: 'Location', field: 'location' },
    {
      headerName: 'Actions',
      field: 'actions',
      cellRendererFramework: (params) => (
        <div>
          <Button onClick={() => handleEditCafe(params.data.id)}>Edit</Button>
          <Button onClick={() => handleDeleteCafe(params.data.id)}>Delete</Button>
        </div>
      ),
    },
  ];

  return (
    <div>
      <h1>Cafes</h1>
      <TextField label="Filter by Location" value={location} onChange={handleLocationChange} />
      <Button onClick={handleAddCafe}>Add New Cafe</Button>
      <div className="ag-theme-alpine" style={{ height: 400, width: '100%' }}>
        <AgGridReact
          rowData={cafes}
          columnDefs={columns}
          pagination={true}
          paginationPageSize={10}
        />
      </div>
    </div>
  );
};

export default CafePage;
