#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <stdint.h>

#include <libpafe.h>
#include <pasori_command.h>
#include <felica_command.h>

typedef struct {
    uint8_t buf[FELICA_BLOCK_LENGTH];
} SFBlock;

typedef struct {
    uint8_t IDm[8];
    int32_t cnt;
    SFBlock entries[20];
} SFDump;

int DumpSFBytes(SFDump *dump, int systemCode, int serviceCode, int retry) {
    pasori *p;
    felica *f;
    int r;

    p = pasori_open();
    if (p == NULL) {
        return 1;
    }
    r = pasori_init(p);
    if (r != 0) {
        return 2;
    }
    for (int i = 0; i < retry; i++) {
        f = felica_polling(p, systemCode, 0, 0);
        if (f != NULL) {
            break;
        }
    }
    if (f == NULL) {
        return 3;
    }
    memcpy(dump->IDm, f->IDm, 8);

    uint8_t block[FELICA_BLOCK_LENGTH];
    int n = 0;
    for (int i = 0; i < 20; i++) {
        r = felica_read_single(f, serviceCode, 0, i, block);
        if (r != 0) {
            continue;
        }
        memcpy(dump->entries[i].buf, block, sizeof(block));
        n++;
    }
    free(f);
	pasori_close(p);
    dump->cnt = n;
	return 0;
}
