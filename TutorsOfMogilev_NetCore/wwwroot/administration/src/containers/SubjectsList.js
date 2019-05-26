import { connect } from 'react-redux';
import { getSortedSubjects } from './../selectors/subjects';
import subjectAC from '../AC/subject';
import EntityList from '../components/EntityList';

const mapStateToProps = state => {
  return {
    items: getSortedSubjects(state),
    loading: state.subjects.loading,
    loaded: state.subjects.loaded,
    loadingFailed: state.subjects.loadingFailed
  };
};

const mapDispatchToProps = dispatch => {
  return {
    loadData: () => {
      dispatch(subjectAC.loadEntities());
    },
    remove: id => {
      dispatch(subjectAC.removeEntity(id));
    },
    create: subject => {
      dispatch(subjectAC.createEntity(subject));
    },
    update: (id, subject) => {
      dispatch(subjectAC.updateEntity(id, subject));
    }
  };
};

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(EntityList);
