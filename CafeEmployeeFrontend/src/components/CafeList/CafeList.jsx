import { useGetCafesQuery, useDeleteCafeMutation } from '../../api/cafeApi';
import { useState } from 'react';
import { AgGridReact } from "ag-grid-react";
import { AllCommunityModule, ModuleRegistry } from 'ag-grid-community';
import { themeMaterial } from 'ag-grid-community'; 
import { useNavigate } from 'react-router-dom';

// Register all Community features
ModuleRegistry.registerModules([AllCommunityModule]);
import { Button, TextField } from '@mui/material';

const CafeList = () => {
  const navigate = useNavigate();
  const [location, setLocation] = useState('');
  const { data: cafes, isLoading, error, refetch } = useGetCafesQuery(location);
  const [deleteCafe] = useDeleteCafeMutation();

  if (isLoading) {
    return <div>Loading...</div>;
  }

  if (error) {
    return <div>Error loading cafes.</div>;
  }

  const handleDelete = async (id) => {
    if (window.confirm('Are you sure you want to delete the cafe?')) {
      let success = await deleteCafe(id);
      if (success) {
        alert('Cafe deleted successfully');
      }
      else {
        alert('Cafe not found');
      }
      refetch();
    }
  };

  const handleLocationChange = (e) => {
    setLocation(e.target.value);
  };

  const columns = [
    {
      headerName: 'Logo',
      field: 'logo',
      flex: 1,
      cellRenderer: (params) => <img src={params.value} alt="logo" style={{
        width: '100%',  // Ensure the image width fits the cell
        height: '100%', // Ensure the image height fits the cell
        objectFit: 'contain',
        // Maintain aspect ratio and fit within the cell
      }}

      />,

      width: 100,
      height: 100,
    },
    {
      headerName: 'Name',
      field: 'name',
      flex: 3, cellStyle: {
        display: 'flex',          // Use flexbox for alignment
        alignItems: 'center',     // Vertically center the content
        justifyContent: 'left', // Horizontally center the content
      },
      
    },
    {
      headerName: 'Description', field: 'description', flex: 3, cellStyle: {
        display: 'flex',          // Use flexbox for alignment
        alignItems: 'center',     // Vertically center the content
        justifyContent: 'left', // Horizontally center the content
      }
    },
    {
      headerName: 'Employees', field: 'id', flex: 2, cellStyle: {
        display: 'flex',          // Use flexbox for alignment
        alignItems: 'center',     // Vertically center the content
        justifyContent: 'left', // Horizontally center the content
      },
      cellRenderer: (params) => (
        <span
          style={{ color: 'blue', cursor: 'pointer' }}
          onClick={() => navigate(`/employee/${params.data.id}`)}
        >
          Employees
        </span>
      ),
    },
    {
      headerName: 'Location', field: 'location', filter: true, flex: 2, cellStyle: {
        display: 'flex',          // Use flexbox for alignment
        alignItems: 'center',     // Vertically center the content
        justifyContent: 'left', // Horizontally center the content
      }
    },
    {
      headerName: 'Actions',
      field: 'id',
      cellStyle: {
        display: 'flex',          // Use flexbox for alignment
        alignItems: 'center',     // Vertically center the content
        justifyContent: 'left', // Horizontally center the content
      },
      cellRenderer: (params) => (
        <div>
          <Button color="primary" onClick={() => navigate(`/cafe/edit/${params.value}`)}>Edit</Button>
          <Button color="error" onClick={() => handleDelete(params.value)}>Delete</Button>
        </div>
      ),
      flex: 2,
    },
  ];

  return (
    <div className="ag-theme-material" style={{ height: 600, width: '1500px' }}>
      <h2>Our Cafes</h2>
      <div style={{ display: 'flex', justifyContent: 'space-between', padding: '10px' }}>
       
        <Button onClick={() => navigate('/cafe/create')} variant="contained">Add New Café</Button>
      </div>

      <AgGridReact
        rowData={cafes}
        columnDefs={columns}
        pagination={true}
        rowHeight={50}
        theme={themeMaterial}
      />
    </div>
  );
};

export default CafeList;
