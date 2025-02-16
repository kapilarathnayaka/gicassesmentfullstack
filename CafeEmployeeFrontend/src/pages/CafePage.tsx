import { useGetCafesQuery, useDeleteCafeMutation } from '../api/cafeApi';
import { useState } from 'react';
import { AgGridReact } from 'ag-grid-react';
import { Button } from '@mui/material';
import 'ag-grid-community/styles/ag-grid.css';
import 'ag-grid-community/styles/ag-theme-material.css';
import { Cafe } from '../types/cafe';
import { ColDef } from 'ag-grid-community';

const CafePage = () => {

  const [location, setLocation] = useState('');
  const { data: cafes, isLoading } = useGetCafesQuery(location);
  const [deleteCafe] = useDeleteCafeMutation();

  const handleDelete = async (id: string) => {
    if (window.confirm('Are you sure?')) {
      await deleteCafe(id);
    }
  };

  // Explicitly type columnDefs
  const columns: ColDef[] = [
    { 
      headerName: 'Logo', 
      field: 'logo', 
      cellRenderer: (params: { value: string }) => <img src={params.value} alt="logo" width={40} /> 
    },
    { headerName: 'Name', field: 'name' },
    { headerName: 'Description', field: 'description' },
    { headerName: 'Employees', field: 'employees' },
    { headerName: 'Location', field: 'location' },
    {
      headerName: 'Actions',
      field: 'id',
      cellRenderer: (params: { value: string }) => (
        <Button color="error" onClick={() => handleDelete(params.value)}>Delete</Button>
      ),
    },
  ];

  return (
    <div className="ag-theme-material" style={{ height: 500, width: '100%' }}>
      <div>should see the cafes here</div>
      {/* <AgGridReact rowData={cafes || []} columnDefs={columns} pagination={true} /> */}
    </div>
  );
};

export default CafePage;
