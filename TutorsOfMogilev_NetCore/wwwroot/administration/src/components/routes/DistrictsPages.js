import React from 'react';
import Container from '../Container';
import DistrictsList from '../../containers/DistrictsList';

export default function DistrictsPage(params) {
  return (
    <Container headerText="Районы">
      <DistrictsList />
    </Container>
  );
}
