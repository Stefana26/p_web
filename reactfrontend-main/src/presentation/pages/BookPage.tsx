import { WebsiteLayout } from "presentation/layouts/WebsiteLayout";
import { Fragment, memo } from "react";
import { Box } from "@mui/material";
import { Seo } from "@presentation/components/ui/Seo";
import { ContentCard } from "@presentation/components/ui/ContentCard";
import { BookTable } from "@presentation/components/ui/Tables/BookTable";

export const BookPage = memo(() => {
    return <Fragment>
      <Seo title="MobyLab Web App | Users" />
      <WebsiteLayout>
        <Box sx={{ padding: "0px 50px 00px 50px", justifyItems: "center" }}>
          <ContentCard>
            <BookTable />
          </ContentCard>
        </Box>
      </WebsiteLayout>
    </Fragment>
  });