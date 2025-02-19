import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react';

export const cafeApi = createApi({
  reducerPath: 'cafeApi',
  baseQuery: fetchBaseQuery({ baseUrl: 'http://localhost:5232/api/' }),
  endpoints: (builder) => ({
    getCafes: builder.query({
    //   query: (location = '') => `Cafe?location=${location}`,
      query: () => `Cafe`,
    }),
    GetCafeById: builder.query({
      query: (id) => `Cafe/${id}`,
    }),
    CreateCafe: builder.mutation({
      query: (newCafe) => ({
        url: 'Cafe',
        method: 'POST',
        body: newCafe,
      }),
    }),
    UpdateCafe: builder.mutation({
      query: (updatedCafe) => ({
        url: `Cafe/${updatedCafe.id}`,
        method: 'PUT',
        body: updatedCafe,
      }),
    }),
    deleteCafe: builder.mutation({
      query: (id) => ({
        url: `Cafe/${id}`,
        method: 'DELETE',
      }),
    }),
  }),
});

export const { useGetCafesQuery, useCreateCafeMutation, useDeleteCafeMutation,useUpdateCafeMutation,useGetCafeByIdQuery } = cafeApi;