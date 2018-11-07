import React from 'react';
import Container from '../Container';
import SubjectsList from '../../containers/SubjectsList';

export default function SubjectsPage(params) {
  return (
    <Container headerText="Предметы">
      <SubjectsList />
    </Container>
  );
}
