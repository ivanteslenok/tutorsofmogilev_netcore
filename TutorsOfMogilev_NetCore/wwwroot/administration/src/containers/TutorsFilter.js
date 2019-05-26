import { connect } from 'react-redux';
import * as tutorAC from '../AC/tutor';
import cityAC from '../AC/city';
import subjectAC from '../AC/subject';
import TutorsFilter from '../components/TutorsFilter';

const mapStateToProps = state => {
  return {
    isLookupValuesLoaded: state.cities.loaded && state.subjects.loaded,
    cities: state.cities.items,
    cityId: state.tutors.filter.cityId,
    subjects: state.subjects.items,
    subjectId: state.tutors.filter.subjectId
  };
};

const mapDispatchToProps = dispatch => {
  return {
    loadLookupValues: () => {
      dispatch(cityAC.loadEntities());
      dispatch(subjectAC.loadEntities());
    },
    setCityId: cityId => {
      dispatch(tutorAC.setFilter({ cityId }));
    },
    setSubjectId: subjectId => {
      dispatch(tutorAC.setFilter({ subjectId }));
    }
  };
};

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(TutorsFilter);
