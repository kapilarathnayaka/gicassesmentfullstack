import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react';

export const employeeApi = createApi({
  reducerPath: 'employeeApi',
  // baseQuery: fetchBaseQuery({ baseUrl: 'http://localhost:5232/api/'}),
  baseQuery: fetchBaseQuery({ baseUrl: 'http://54.82.91.72:8080/api/' }),
  endpoints: (builder) => ({
    getEmployees: builder.query({
      query: () => `employees`,
    }),
    getEmployeesByCafeId: builder.query({
      query: (id) => `employees/by-cafe/${id}`,
    }),
    getEmployeeById: builder.query({
      query: (id) => `employees/${id}`,
    }),
    createEmployee: builder.mutation({
      query: (newEmployee) => ({
        url: 'employees',
        method: 'POST',
        body: newEmployee,
      }),
    }),
    UpdateEmployee: builder.mutation({    
      query: (updatedEmployee) => ({
        url: `employees/${updatedEmployee.id}`,
        method: 'PUT',
        body: updatedEmployee,
      }),
    }),
    deleteEmployee: builder.mutation({
      query: (id) => ({
        url: `employees/${id}`,
        method: 'DELETE',
      }),
    }),
  }),
});

export const { 
  useGetEmployeesQuery, 
  useCreateEmployeeMutation, 
  useUpdateEmployeeMutation,
  useGetEmployeesByCafeIdQuery,
  useGetEmployeeByIdQuery,
  useDeleteEmployeeMutation 
} = employeeApi;