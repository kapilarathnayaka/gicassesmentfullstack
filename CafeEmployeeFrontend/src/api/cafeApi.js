import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react';

export const cafeApi = createApi({
  reducerPath: 'cafeApi',
  // baseQuery: fetchBaseQuery({ baseUrl: 'http://localhost:5232/api/' }),
  baseQuery: fetchBaseQuery({ baseUrl: 'http://54.82.91.72:8080/api/' }),
  endpoints: (builder) => ({
    getCafes: builder.query({
      //query: (location = '') => `Cafe?location=${location}`,
      query: () => `cafes`,
    }),
    GetCafeById: builder.query({
      query: (id) => `cafes/${id}`,
    }),
    CreateCafe: builder.mutation({
      query: (newCafe) => ({
        url: 'cafes',
        method: 'POST',
        body: newCafe,
      }),
    }),
    UpdateCafe: builder.mutation({
      query: (updatedCafe) => ({
        url: `cafes/${updatedCafe.id}`,
        method: 'PUT',
        body: updatedCafe,
      }),
    }),
    deleteCafe: builder.mutation({
      query: (id) => ({
        url: `cafes/${id}`,
        method: 'DELETE',
      }),
    }),
  }),
});

export const { useGetCafesQuery, useCreateCafeMutation, useDeleteCafeMutation,useUpdateCafeMutation,useGetCafeByIdQuery } = cafeApi;