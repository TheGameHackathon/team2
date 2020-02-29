import React from 'react';
import styles from './styles.css'

class Row extends React.Component {
    constructor(props) {
        super(props);
        this.props = props
    }

    render() {
        let cells = []
        for (let cell of this.props.cells) {
            if (cell === '')
                cells.push(<td></td>)
            else
                cells.push(<td className={cell}></td>)

        }
        console.log(cells)
        return (
            <tr>
                {cells}
            </tr>
        );
    }
}

class Map extends React.Component {
    render() {
        return (
            <table className={styles.field}>
                <tbody>
                    <Row cells={[styles.empty, styles.empty, styles.wall, styles.wall, styles.wall, styles.wall, styles.wall, styles.empty]} />
                    <Row cells={[styles.wall, styles.wall, styles.wall, styles.empty, styles.empty, styles.empty, styles.wall, styles.empty]} />
                    <Row cells={[styles.wall, styles.target, styles.empty, styles.empty, styles.empty, styles.empty, styles.wall, styles.empty]} />
                    <Row cells={[styles.wall, styles.wall, styles.wall, styles.empty, styles.box, styles.target, styles.wall, styles.empty]} />
                    <Row cells={[styles.wall, styles.target, styles.wall, styles.wall, styles.box, styles.empty, styles.wall, styles.empty]} />
                    <Row cells={[styles.wall, styles.empty, styles.wall, styles.empty, styles.target, styles.empty, styles.wall, styles.wall]} />
                    <Row cells={[styles.wall, styles.empty, styles.empty, styles.empty, styles.empty, styles.empty, styles.target, styles.wall]} />
                    <Row cells={[styles.wall, styles.empty, styles.empty, styles.empty, styles.target, styles.empty, styles.empty, styles.wall]} />
                    <Row cells={[styles.wall, styles.wall, styles.wall, styles.wall, styles.wall, styles.wall, styles.wall, styles.wall]} />
                </tbody>
            </table>);
    }

    componentDidMount() {


    }
}

class Player extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            top: 2,
            left: 3
        };
        this.cellSize = props.cellSize;
    }

    render() {
        debugger;
        const style = {
            width: this.cellSize + 'px',
            height: this.cellSize + 'px',
            top: this.state.top * this.cellSize + 'px',
            left: this.state.left * this.cellSize + 'px',
            position: 'absolute'
        }
        return (
            <div className={styles.player} style={style} id="player"></div>);
    }
}


function parseMap(jsonMap) {
    const values = {
        "W": styles.wall,
        "E": styles.empty,
        "P": styles.player,
        "T": styles.target,
        "B": styles.box
    }


}

export default class Field extends React.Component {
    constructor(props) {
        super(props);
        this.cellSize = props.cellSize;
    }

    render() {
        return (
            <div className={styles.center}>
                <div className={styles.fieldWrapper}>
                    <Map />
                    <Player cellSize={this.cellSize} />
                </div>
            </div>
        );
    }
}
