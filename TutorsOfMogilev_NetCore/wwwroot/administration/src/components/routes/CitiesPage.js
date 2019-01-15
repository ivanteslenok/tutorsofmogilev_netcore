import React from 'react';
import Container from '../Container';
import CitiesList from '../../containers/CitiesList';

export default function CitiesPage(params) {
  return (
    <Container headerText="Города">
      <CitiesList />
    </Container>
  );
}
