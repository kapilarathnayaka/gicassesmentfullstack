import { useGetEmployeesQuery, useDeleteEmployeeMutation } from '../api/employeeApi';
import { AgGridReact } from 'ag-grid-react';
import { Button } from '@mui/material';
import 'ag-grid-community/styles/ag-grid.css';
import 'ag-grid-community/styles/ag-theme-material.css';
import { Employee } from '../types/employee';
import { ColDef } from 'ag-grid-community';

const EmployeePage = () => {
  const { data: employees, isLoading } = useGetEmployeesQuery();
  const [deleteEmployee] = useDeleteEmployeeMutation();

  const handleDelete = async (id: string) => {
    if (window.confirm('Are you sure?')) {
      await deleteEmployee(id);
    }
  };

  // Explicitly typing the column definitions
  const columns: ColDef[] = [
    { headerName: 'Employee ID', field: 'id' },
    { headerName: 'Name', field: 'name' },
    { headerName: 'Email Address', field: 'email_address' },
    { headerName: 'Phone Number', field: 'phone_number' },
    { headerName: 'Days Worked', field: 'days_worked' },
    { headerName: 'Cafe', field: 'cafe' },
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
      {/* Loading State Handling */}
      {isLoading ? (
        <div>Loading...</div>
      ) : (
        <AgGridReact
          rowData={employees || []}
          columnDefs={columns}
          pagination={true}
          paginationPageSize={10}
        />
      )}
    </div>
  );
};

export default EmployeePage;
