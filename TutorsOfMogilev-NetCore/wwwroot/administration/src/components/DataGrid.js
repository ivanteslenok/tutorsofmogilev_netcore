import React, { PureComponent } from 'react';
import Paper from '@material-ui/core/Paper';
import {
  PagingState,
  SortingState,
  CustomPaging
} from '@devexpress/dx-react-grid';
import {
  Grid,
  Table,
  TableHeaderRow,
  PagingPanel
} from '@devexpress/dx-react-grid-material-ui';
import Loading from './Loading';

export default class DataGrid extends PureComponent {
  state = {
    columns: [],
    rows: [],
    sorting: [{ columnName: 'lastName', direction: 'asc' }],
    totalCount: 0,
    pageSize: 10,
    pageSizes: [5, 10, 15],
    currentPage: 0,
    loading: true
  };

  static getDerivedStateFromProps(props, state) {
    return {
      columns: props.gridColumns,
      rows: props.items,
      totalCount: props.totalCount,
      loading: props.loading
    };
  }

  componentDidMount() {
    this.loadDataForGrid();
  }

  componentDidUpdate() {
    this.loadDataForGrid();
  }

  getQueryParams() {
    const { sorting, pageSize, currentPage } = this.state;
    const columnSorting = sorting[0];

    const filter = {
      pageSize,
      pageNumber: currentPage + 1,
      sortBy: columnSorting.columnName,
      sortDirection: columnSorting.direction === 'desc' ? 'desc' : ''
    };

    let queryParams = '?';

    for (let key in filter) {
      queryParams += filter[key] ? `${key}=${filter[key]}&` : '';
    }

    // чтобы убрать последний & или ? если нету параметров
    queryParams = queryParams.substring(0, queryParams.length - 1);

    return queryParams;
  }

  loadDataForGrid() {
    const queryParams = this.getQueryParams();
    const { loading, loadData } = this.props;

    if (loading || queryParams === this.lastQueryParams) return;

    loadData(queryParams);

    this.lastQueryParams = queryParams;
  }

  changeSorting = sorting => {
    this.setState({
      loading: true,
      sorting
    });
  };

  changeCurrentPage = currentPage => {
    this.setState({
      loading: true,
      currentPage
    });
  };

  changePageSize = pageSize => {
    const { totalCount, currentPage: stateCurrentPage } = this.state;
    const totalPages = Math.ceil(totalCount / pageSize);
    const currentPage = Math.min(stateCurrentPage, totalPages - 1);

    this.setState({
      loading: true,
      pageSize,
      currentPage
    });
  };

  render() {
    const {
      rows,
      columns,
      sorting,
      pageSize,
      pageSizes,
      currentPage,
      totalCount,
      loading
    } = this.state;

    return (
      <Paper style={{ position: 'relative' }}>
        <Grid rows={rows} columns={columns}>
          <SortingState
            sorting={sorting}
            onSortingChange={this.changeSorting}
          />
          <PagingState
            currentPage={currentPage}
            onCurrentPageChange={this.changeCurrentPage}
            pageSize={pageSize}
            onPageSizeChange={this.changePageSize}
          />
          <CustomPaging totalCount={totalCount} />
          <Table />
          <TableHeaderRow showSortingControls />
          <PagingPanel pageSizes={pageSizes} />
        </Grid>
          {loading && <Loading />}
      </Paper>
    );
  }
}
