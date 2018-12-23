import React from 'react';
import Container from '../Container';
import SpecializationsList from '../../containers/SpecializationsList';

export default function SpecializationsPage(params) {
  return (
    <Container headerText="Специализации">
      <SpecializationsList />
    </Container>
  );
}
