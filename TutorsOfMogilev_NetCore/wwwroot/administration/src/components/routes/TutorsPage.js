import React from 'react';
import { OPTIMIZE_TUTORS_PHOTOS_URL } from "../../constants";
import Container from '../Container';
import TutorsFilter from '../../containers/TutorsFilter';
import TutorsDataGrid from '../../containers/TutorsDataGrid';

const TutorsPage = () => (
  <Container headerText="Репетиторы">
    <TutorsFilter />
    <TutorsDataGrid />
    <a href={OPTIMIZE_TUTORS_PHOTOS_URL}>Optimize tutors photos</a>
  </Container>
);

export default TutorsPage;
