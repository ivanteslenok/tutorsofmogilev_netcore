import React from 'react';
import Container from '../Container';
import TutorsDataGrid from '../../containers/TutorsDataGrid';

export default function TutorsPage(params) {
  return (
    <Container headerText="Репетиторы">
      <TutorsDataGrid />
    </Container>
  )
}
