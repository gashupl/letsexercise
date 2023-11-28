import * as React from 'react';
import { Label } from '@fluentui/react';
import { Progress } from 'rsuite';

const style = {
  width: 120,
  display: 'inline-block',
  marginRight: 10
};

export interface ICircleProgressProps {
  percentage?: number;
}

export class CircleProgress extends React.Component<ICircleProgressProps> {
  public render(): React.ReactNode {
    return (
      <div style={style}>
        <Progress.Circle percent={this.props.percentage} status="success" />
        {/* <Label>
        {this.props.percentage}
      </Label> */}
    </div>
    )
  }
}
