import React from 'react';
import Container from '../Container';
import TutorsFilter from '../../containers/TutorsFilter';
import TutorsDataGrid from '../../containers/TutorsDataGrid';

const TutorsPage = () => (
  <Container headerText="Репетиторы">
    <TutorsFilter />
    <TutorsDataGrid />
  </Container>
);

export default TutorsPage;
