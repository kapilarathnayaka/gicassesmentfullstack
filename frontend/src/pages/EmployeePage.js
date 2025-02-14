import React, { useState, useEffect } from 'react';
import { useHistory } from 'react-router-dom';
import { AgGridReact } from 'ag-grid-react';
import 'ag-grid-community/styles/ag-grid.css';
import 'ag-grid-community/styles/ag-theme-alpine.css';
import { Button, TextField } from '@material-ui/core';
import axios from 'axios';

const EmployeePage = () => {
  const [employees, setEmployees] = useState([]);
  const [cafe, setCafe] = useState('');
  const history = useHistory();

  useEffect(() => {
    fetchEmployees();
  }, [cafe]);

  const fetchEmployees = async () => {
    try {
      const response = await axios.get(`/api/employee?cafe=${cafe}`);
      setEmployees(response.data);
    } catch (error) {
      console.error('Error fetching employees:', error);
    }
  };

  const handleCafeChange = (event) => {
    setCafe(event.target.value);
  };

  const handleAddEmployee = () => {
    history.push('/add-edit-employee');
  };

  const handleEditEmployee = (id) => {
    history.push(`/add-edit-employee/${id}`);
  };

  const handleDeleteEmployee = async (id) => {
    if (window.confirm('Are you sure you want to delete this employee?')) {
      try {
        await axios.delete(`/api/employee/${id}`);
        fetchEmployees();
      } catch (error) {
        console.error('Error deleting employee:', error);
      }
    }
  };

  const columns = [
    { headerName: 'Employee ID', field: 'id' },
    { headerName: 'Name', field: 'name' },
    { headerName: 'Email Address', field: 'emailAddress' },
    { headerName: 'Phone Number', field: 'phoneNumber' },
    { headerName: 'Days Worked', field: 'daysWorked' },
    { headerName: 'Cafe', field: 'cafe' },
    {
      headerName: 'Actions',
      field: 'actions',
      cellRendererFramework: (params) => (
        <div>
          <Button onClick={() => handleEditEmployee(params.data.id)}>Edit</Button>
          <Button onClick={() => handleDeleteEmployee(params.data.id)}>Delete</Button>
        </div>
      ),
    },
  ];

  return (
    <div>
      <h1>Employees</h1>
      <TextField label="Filter by Cafe" value={cafe} onChange={handleCafeChange} />
      <Button onClick={handleAddEmployee}>Add New Employee</Button>
      <div className="ag-theme-alpine" style={{ height: 400, width: '100%' }}>
        <AgGridReact
          rowData={employees}
          columnDefs={columns}
          pagination={true}
          paginationPageSize={10}
        />
      </div>
    </div>
  );
};

export default EmployeePage;
