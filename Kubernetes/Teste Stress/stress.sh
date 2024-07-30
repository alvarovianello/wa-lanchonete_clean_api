
#!/bin/bash

for i in {1..10000}; do

    curl http://localhost:3001/swagger/index.html
    sleep $1

done

